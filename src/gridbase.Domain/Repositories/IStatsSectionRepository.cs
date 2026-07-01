using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IStatsSectionRepository
{
    Task<StatsSection?> GetAsync();
    Task AddAsync(StatsSection entity);
    Task SaveChangesAsync();
}