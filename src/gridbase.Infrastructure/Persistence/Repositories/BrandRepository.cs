using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class BrandRepository(GridBaseDbContext db) : IBrandRepository
{
    public Task<BrandConfig?> GetAsync() =>
        db.BrandConfigs.FirstOrDefaultAsync();

    public async Task AddAsync(BrandConfig entity) =>
        await db.BrandConfigs.AddAsync(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}