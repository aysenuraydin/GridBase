using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IContactRepository
{
    Task<ContactConfig?> GetAsync();
    Task AddAsync(ContactConfig entity);
    Task SaveChangesAsync();
}