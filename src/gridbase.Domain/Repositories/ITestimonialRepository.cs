using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface ITestimonialRepository
{
    Task<List<Testimonial>> GetAllAsync();
    Task<Testimonial?> GetByExternalIdAsync(string externalId);
    Task AddAsync(Testimonial entity);
    void Remove(Testimonial entity);
    Task SaveChangesAsync();
}

