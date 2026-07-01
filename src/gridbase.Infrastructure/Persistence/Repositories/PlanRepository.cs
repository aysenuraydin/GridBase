using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class PlanRepository(GridBaseDbContext db) : IPlanRepository
{
    public Task<PlanSection?> GetWithItemsAsync() =>
        db.PlanSections
            .Include(x => x.Items)
            .ThenInclude(x => x.Features)
            .FirstOrDefaultAsync();

    public async Task AddAsync(PlanSection section) =>
        await db.PlanSections.AddAsync(section);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}