using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IBrandRepository
{
    Task<BrandConfig?> GetAsync();
    Task AddAsync(BrandConfig entity);
    Task SaveChangesAsync();
}