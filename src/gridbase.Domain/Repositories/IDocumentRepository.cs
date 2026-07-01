using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IDocumentRepository
{
    Task<Document?> GetAsync();
    Task AddAsync(Document entity);
    Task SaveChangesAsync();
}
