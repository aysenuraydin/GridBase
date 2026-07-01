using gridbase.Application.Common.Interfaces;
using gridbase.Application.Interfaces;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;
using gridbase.Domain.Enums;
using gridbase.Domain.Repositories;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _uow;
    private readonly IUser _currentUser;
    private readonly IGridBaseService _gridBaseService;
    private readonly IApiKeyRepository _apiKeyRepo;

    public ProjectService(
        IUnitOfWork uow,
        IUser currentUser,
        IGridBaseService gridBaseService,
        IApiKeyRepository apiKeyRepo)
    {
        _uow = uow;
        _currentUser = currentUser;
        _gridBaseService = gridBaseService;
        _apiKeyRepo = apiKeyRepo;
    }

    private bool IsAdmin =>
        string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase)
        || string.Equals(_currentUser.Role, "GB", StringComparison.OrdinalIgnoreCase);

    private string RequireUserId()
    {
        if (string.IsNullOrEmpty(_currentUser.Id))
            throw new UnauthorizedAccessException("Giris yapmalisiniz.");
        return _currentUser.Id;
    }

    private void EnsureOwner(Project project)
    {
        if (IsAdmin) return;
        if (!string.Equals(project.OwnerUserId, _currentUser.Id, StringComparison.Ordinal))
            throw new UnauthorizedAccessException("Bu projeye erisim yetkiniz yok.");
    }

    private static PlanType CurrentPlan => PlanType.Free;
    public async Task<IReadOnlyList<ProjectListItem>> GetMyProjectsAsync(CancellationToken ct = default)
    {
        var userId = RequireUserId();
        var projects = await _uow.ProjectRepository.GetByOwnerAsync(userId, ct);

        var list = new List<ProjectListItem>();
        foreach (var p in projects)
        {
            var tableCount = await _uow.ProjectRepository.CountTablesAsync(p.Id, ct);
            list.Add(new ProjectListItem
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Plan = p.Plan,
                TableCount = tableCount,
                CreatedAt = p.CreatedAt
            });
        }
        return list;
    }
    public async Task<ProjectResponse?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        var project = await _uow.ProjectRepository.GetByIdAsync(id, ct);
        if (project is null) return null;

        EnsureOwner(project);

        var tableCount = await _uow.ProjectRepository.CountTablesAsync(project.Id, ct);
        return ToResponse(project, tableCount);
    }
    public async Task<ProjectQuotaResponse> GetQuotaAsync(CancellationToken ct = default)
    {
        var userId = RequireUserId();
        var used = await _uow.ProjectRepository.CountByOwnerAsync(userId, ct);
        var max = PlanLimits.For(CurrentPlan).MaxProjects;

        return new ProjectQuotaResponse
        {
            Used = used,
            Max = max,
            CanCreate = IsAdmin || used < max,
            Plan = CurrentPlan
        };
    }
    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct = default)
    {
        var userId = RequireUserId();

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Proje adi zorunludur.", nameof(request));

        var name = request.Name.Trim();

        if (!IsAdmin)
        {
            var used = await _uow.ProjectRepository.CountByOwnerAsync(userId, ct);
            var max = PlanLimits.For(CurrentPlan).MaxProjects;
            if (used >= max)
                throw new InvalidOperationException(
                    $"Ucretsiz planda en fazla {max} proje olusturabilirsiniz. Mevcut: {used}.");
        }

        if (await _uow.ProjectRepository.NameExistsForOwnerAsync(name, userId, null, ct))
            throw new InvalidOperationException($"'{name}' adinda bir projeniz zaten var.");

        var project = Project.Create(name, userId, CurrentPlan, request.Description);

        await _uow.ProjectRepository.AddAsync(project, ct);
        await _uow.CommitAsync(ct);

        return ToResponse(project, 0);
    }
    public async Task<ProjectResponse?> UpdateAsync(long id, UpdateProjectRequest request, CancellationToken ct = default)
    {
        var userId = RequireUserId();

        var project = await _uow.ProjectRepository.GetByIdAsync(id, ct);
        if (project is null) return null;

        EnsureOwner(project);

        var newName = string.IsNullOrWhiteSpace(request.Name) ? project.Name : request.Name.Trim();

        if (!string.Equals(newName, project.Name, StringComparison.Ordinal)
            && await _uow.ProjectRepository.NameExistsForOwnerAsync(newName, project.OwnerUserId, project.Id, ct))
        {
            throw new InvalidOperationException($"'{newName}' adinda bir projeniz zaten var.");
        }

        project.Update(newName, request.Description ?? project.Description);
        await _uow.CommitAsync(ct);

        var tableCount = await _uow.ProjectRepository.CountTablesAsync(project.Id, ct);
        return ToResponse(project, tableCount);
    }
    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
        var project = await _uow.ProjectRepository.GetByIdAsync(id, ct);
        if (project is null) return false;

        EnsureOwner(project);

        var tables = _uow.TableRepository.GetAll()
            .Where(t => t.ProjectId == id)
            .ToList();

        foreach (var t in tables)
        {
            t.HardDelete();
            await _uow.TableRepository.Delete(t);
        }

        var keys = await _apiKeyRepo.GetByProjectAsync(id, ct);
        foreach (var k in keys)
        {
            k.HardDelete();
            _apiKeyRepo.Remove(k);
        }

        project.HardDelete();
        _uow.ProjectRepository.Remove(project);

        await _uow.CommitAsync(ct);

        return true;
    }
    public async Task<ProjectOverviewResponse> GetOverviewAsync(long projectId, CancellationToken ct = default)
    {
        var project = await _uow.ProjectRepository.GetByIdAsync(projectId, ct)
            ?? throw new KeyNotFoundException("Proje bulunamadi.");
        EnsureOwner(project);

        var tableCount = await _uow.ProjectRepository.CountTablesAsync(projectId, ct);
        var totalRows = await _uow.ProjectRepository.CountRowsAsync(projectId, ct);
        var recent = await _uow.ProjectRepository.GetRecentTablesAsync(projectId, 5, ct);

        var keys = await _apiKeyRepo.GetByProjectAsync(projectId, ct);
        var activeKeys = keys.Count(k => k.IsActive);

        var limits = PlanLimits.For(project.Plan);

        return new ProjectOverviewResponse
        {
            ProjectId = project.Id,
            ProjectName = project.Name,
            Plan = project.Plan.ToString(),
            TableCount = tableCount,
            TotalRows = totalRows,
            FileCount = 0,
            StorageBytes = 0,
            ActiveKeyCount = activeKeys,
            MaxTables = limits.MaxTablesPerProject,
            MaxStorageMb = limits.MaxStorageMb,
            CreatedAt = project.CreatedAt,
            RecentTables = recent.Select(t => new OverviewTableItem
            {
                Id = t.Id,
                Name = t.Name,
                RowCount = t.RowsFk?.Count(r => r.DeletedAt == null) ?? 0,
                ColumnCount = t.ColumnsFk?.Count(c => c.DeletedAt == null && c.RealColumnId == null) ?? 0
            }).ToList()
        };
    }

    private static ProjectResponse ToResponse(Project p, int tableCount) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        Plan = p.Plan,
        TableCount = tableCount,
        CreatedAt = p.CreatedAt,
        UpdatedAt = p.LastModifiedAt
    };
}