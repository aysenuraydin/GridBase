using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class StatsSectionRepository(GridBaseDbContext db) : IStatsSectionRepository
{
    public Task<StatsSection?> GetAsync() =>
        db.StatsSections.FirstOrDefaultAsync();

    public async Task AddAsync(StatsSection entity) =>
        await db.StatsSections.AddAsync(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}

