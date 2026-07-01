using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface ISocialLinkRepository
{
    Task<List<SocialLink>> GetAllAsync();
    Task<SocialLink?> GetByIdAsync(int id);
    Task AddAsync(SocialLink entity);
    void Remove(SocialLink entity);
    Task SaveChangesAsync();
}