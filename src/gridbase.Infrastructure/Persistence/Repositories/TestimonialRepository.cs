using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class TestimonialRepository(GridBaseDbContext db) : ITestimonialRepository
{
    public Task<List<Testimonial>> GetAllAsync() =>
        db.Testimonials.ToListAsync();

    public Task<Testimonial?> GetByExternalIdAsync(string externalId) =>
        db.Testimonials.FirstOrDefaultAsync(x => x.ExternalId == externalId);

    public async Task AddAsync(Testimonial entity) =>
        await db.Testimonials.AddAsync(entity);

    public void Remove(Testimonial entity) =>
        db.Testimonials.Remove(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}


