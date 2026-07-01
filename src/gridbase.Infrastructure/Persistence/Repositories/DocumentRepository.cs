using Microsoft.EntityFrameworkCore;
using gridbase.Application.Services;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class DocumentRepository(GridBaseDbContext db) : IDocumentRepository
{
    public Task<Document?> GetAsync() =>
        db.Documents.FirstOrDefaultAsync();

    public async Task AddAsync(Document entity) =>
        await db.Documents.AddAsync(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}
