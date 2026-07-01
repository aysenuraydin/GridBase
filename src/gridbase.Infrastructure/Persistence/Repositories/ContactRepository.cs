using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ContactRepository(GridBaseDbContext db) : IContactRepository
{
    public Task<ContactConfig?> GetAsync() =>
        db.ContactConfigs.FirstOrDefaultAsync();

    public async Task AddAsync(ContactConfig entity) =>
        await db.ContactConfigs.AddAsync(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}