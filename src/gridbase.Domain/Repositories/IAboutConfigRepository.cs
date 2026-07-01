using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IAboutConfigRepository
{
    Task<AboutConfig?> GetAsync();
    Task AddAsync(AboutConfig entity);
    Task SaveChangesAsync();
}
