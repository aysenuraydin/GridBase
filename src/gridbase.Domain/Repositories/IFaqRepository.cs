using gridbase.Domain.Entities;
namespace gridbase.Domain.Repositories;

public interface IFaqRepository
{
    Task<List<FaqCategory>> GetAllWithQuestionsAsync();
    Task RemoveRangeAsync(List<FaqCategory> categories);
    Task AddRangeAsync(List<FaqCategory> categories);
    Task SaveChangesAsync();
}