using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ProjectCorsRepository : BaseRepository<ProjectCorsOrigin, long>, IProjectCorsRepository
{
    private readonly GridBaseDbContext _context;

    public ProjectCorsRepository(GridBaseDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ProjectCorsOrigin>> GetByProjectAsync(long projectId, CancellationToken ct = default) =>
        await _context.Set<ProjectCorsOrigin>()
            .Where(o => o.ProjectId == projectId && o.DeletedAt == null)
            .OrderBy(o => o.Origin)
            .ToListAsync(ct);

    public async Task<ProjectCorsOrigin?> GetByIdAsync(long id, CancellationToken ct = default) =>
        await _context.Set<ProjectCorsOrigin>()
            .FirstOrDefaultAsync(o => o.Id == id && o.DeletedAt == null, ct);

    public async Task<bool> IsOriginAllowedAsync(long projectId, string origin, CancellationToken ct = default)
    {
        var norm = ProjectCorsOrigin.Normalize(origin);

        return await _context.Set<ProjectCorsOrigin>()
            .AnyAsync(o => o.ProjectId == projectId
                && o.DeletedAt == null
                && (o.Origin == "*" || o.Origin == norm), ct);
    }

    public async Task<bool> ExistsAsync(long projectId, string origin, CancellationToken ct = default)
    {
        var norm = ProjectCorsOrigin.Normalize(origin);
        return await _context.Set<ProjectCorsOrigin>()
            .AnyAsync(o => o.ProjectId == projectId && o.DeletedAt == null && o.Origin == norm, ct);
    }

    public async Task AddAsync(ProjectCorsOrigin origin, CancellationToken ct = default) =>
        await _context.Set<ProjectCorsOrigin>().AddAsync(origin, ct);

    public void Remove(ProjectCorsOrigin origin) =>
        _context.Set<ProjectCorsOrigin>().Remove(origin);
}