using Microsoft.EntityFrameworkCore;
using gridbase.Application.Services;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class AboutConfigRepository(GridBaseDbContext db) : IAboutConfigRepository
{
    public Task<AboutConfig?> GetAsync() =>
        db.AboutConfigs.FirstOrDefaultAsync();

    public async Task AddAsync(AboutConfig entity) =>
        await db.AboutConfigs.AddAsync(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}
