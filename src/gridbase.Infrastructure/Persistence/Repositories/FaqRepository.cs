using Microsoft.EntityFrameworkCore;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class FaqRepository(GridBaseDbContext db) : IFaqRepository
{
    public Task<List<FaqCategory>> GetAllWithQuestionsAsync() =>
        db.FaqCategories
            .Include(x => x.Questions)
            .ToListAsync();

    public Task RemoveRangeAsync(List<FaqCategory> categories)
    {
        db.FaqCategories.RemoveRange(categories);
        return Task.CompletedTask;
    }

    public async Task AddRangeAsync(List<FaqCategory> categories) =>
        await db.FaqCategories.AddRangeAsync(categories);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}
