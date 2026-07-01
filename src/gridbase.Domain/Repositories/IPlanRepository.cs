using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IPlanRepository
{
    Task<PlanSection?> GetWithItemsAsync();
    Task AddAsync(PlanSection section);
    Task SaveChangesAsync();
}