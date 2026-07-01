using gridbase.Application.Common.Interfaces;
using gridbase.Application.Services;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.DTO.DTOs;
public class ProjectCorsService : IProjectCorsService
{
    private readonly IProjectCorsRepository _corsRepo;
    private readonly IProjectRepository _projectRepo;
    private readonly IUnitOfWork _uow;
    private readonly IUser _currentUser;

    public ProjectCorsService(
        IProjectCorsRepository corsRepo, IProjectRepository projectRepo,
        IUnitOfWork uow, IUser currentUser)
    {
        _corsRepo = corsRepo; _projectRepo = projectRepo;
        _uow = uow; _currentUser = currentUser;
    }

    private bool IsAdmin =>
        string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase)
        || string.Equals(_currentUser.Role, "GB", StringComparison.OrdinalIgnoreCase);

    private async Task<Project> EnsureOwnedAsync(long projectId, CancellationToken ct)
    {
        // 🔒 Hidden. Projeyi getir → yoksa NotFound → sahiplik değilse Unauthorized.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<IReadOnlyList<CorsOriginItem>> GetByProjectAsync(long projectId, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: sahiplik → origin'leri çek → DTO.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<CorsOriginItem> AddAsync(long projectId, AddCorsOriginRequest request, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: sahiplik → origin normalize → şema doğrula (* veya http/https)
        //   → benzersizlik → oluştur/kaydet → DTO.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<bool> RemoveAsync(long projectId, long originId, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: sahiplik → origin'i bul/projeye ait mi → sil → kaydet.
        throw new NotImplementedException("Source available on request.");
    }
}