using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class GalleryItemRepository(GridBaseDbContext db) : IGalleryItemRepository
{
    public Task<List<GalleryItem>> GetAllAsync() =>
        db.GalleryItems.ToListAsync();

    public Task<GalleryItem?> GetByIdAsync(int id) =>
        db.GalleryItems.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(GalleryItem entity) =>
        await db.GalleryItems.AddAsync(entity);

    public void Remove(GalleryItem entity) =>
        db.GalleryItems.Remove(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}
