using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ApiKeyRepository : BaseRepository<ApiKey, long>, IApiKeyRepository
{
    private readonly GridBaseDbContext _context;

    public ApiKeyRepository(GridBaseDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ApiKey?> GetByHashAsync(string keyHash, CancellationToken ct = default) =>
        await _context.Set<ApiKey>()
            .FirstOrDefaultAsync(k =>
                k.KeyHash == keyHash
                && k.RevokedAt == null
                && k.DeletedAt == null, ct);

    public async Task<IReadOnlyList<ApiKey>> GetByProjectAsync(long projectId, CancellationToken ct = default) =>
        await _context.Set<ApiKey>()
            .Where(k => k.ProjectId == projectId && k.DeletedAt == null)
            .OrderByDescending(k => k.CreatedAt)
            .ToListAsync(ct);

    public async Task<ApiKey?> GetByIdAsync(long id, CancellationToken ct = default) =>
        await _context.Set<ApiKey>()
            .FirstOrDefaultAsync(k => k.Id == id && k.DeletedAt == null, ct);

    public async Task AddAsync(ApiKey apiKey, CancellationToken ct = default) =>
        await _context.Set<ApiKey>().AddAsync(apiKey, ct);

    public void Remove(ApiKey apiKey) =>
        _context.Set<ApiKey>().Remove(apiKey);
}