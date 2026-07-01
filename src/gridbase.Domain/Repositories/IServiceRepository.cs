using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IServiceRepository
{
    Task<ServiceSection?> GetWithItemsAsync();
    Task AddAsync(ServiceSection section);
    Task SaveChangesAsync();
}