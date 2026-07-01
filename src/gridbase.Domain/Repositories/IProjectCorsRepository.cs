using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IProjectCorsRepository
{
    Task<IReadOnlyList<ProjectCorsOrigin>> GetByProjectAsync(long projectId, CancellationToken ct = default);

    Task<ProjectCorsOrigin?> GetByIdAsync(long id, CancellationToken ct = default);

    Task<bool> IsOriginAllowedAsync(long projectId, string origin, CancellationToken ct = default);

    Task<bool> ExistsAsync(long projectId, string origin, CancellationToken ct = default);

    Task AddAsync(ProjectCorsOrigin origin, CancellationToken ct = default);
    void Remove(ProjectCorsOrigin origin);
}