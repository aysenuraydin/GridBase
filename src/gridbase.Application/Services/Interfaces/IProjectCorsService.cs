using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public interface IProjectCorsService
{
    Task<IReadOnlyList<CorsOriginItem>> GetByProjectAsync(long projectId, CancellationToken ct = default);
    Task<CorsOriginItem> AddAsync(long projectId, AddCorsOriginRequest request, CancellationToken ct = default);
    Task<bool> RemoveAsync(long projectId, long originId, CancellationToken ct = default);
}