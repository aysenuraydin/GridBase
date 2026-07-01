using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class TenantConfigRepository : ITenantConfigRepository
{
    private readonly GridBaseDbContext _context;

    public TenantConfigRepository(GridBaseDbContext context)
    {
        _context = context;
    }

    public Task<TenantConfig?> GetAsync(CancellationToken ct = default)
        => _context.Set<TenantConfig>().SingleOrDefaultAsync(ct);

    public async Task<TenantConfig> AddAsync(TenantConfig config, CancellationToken ct = default)
    {
        _context.Set<TenantConfig>().Add(config);
        return config;
    }

    public Task UpdateAsync(TenantConfig config, CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}