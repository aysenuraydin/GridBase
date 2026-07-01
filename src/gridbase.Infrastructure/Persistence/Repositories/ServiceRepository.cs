using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ServiceRepository(GridBaseDbContext db) : IServiceRepository
{
    public Task<ServiceSection?> GetWithItemsAsync() =>
        db.ServiceSections
            .Include(x => x.Items)
            .FirstOrDefaultAsync();

    public async Task AddAsync(ServiceSection section) =>
        await db.ServiceSections.AddAsync(section);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}
