using gridbase.DTO.DTOs;

namespace gridbase.Application.Services.Interfaces;

public interface IProjectService
{
    Task<IReadOnlyList<ProjectListItem>> GetMyProjectsAsync(CancellationToken ct = default);

    Task<ProjectResponse?> GetByIdAsync(long id, CancellationToken ct = default);

    Task<ProjectQuotaResponse> GetQuotaAsync(CancellationToken ct = default);

    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct = default);

    Task<ProjectResponse?> UpdateAsync(long id, UpdateProjectRequest request, CancellationToken ct = default);

    Task<bool> DeleteAsync(long id, CancellationToken ct = default);

    Task<ProjectOverviewResponse> GetOverviewAsync(long projectId, CancellationToken ct = default);
}