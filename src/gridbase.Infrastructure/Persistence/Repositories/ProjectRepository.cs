using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.Infrastructure.Persistence.Common.Repositories;

namespace gridbase.Infrastructure.Persistence.Repositories;

public class ProjectRepository : BaseRepository<Project, long>, IProjectRepository
{
    private readonly GridBaseDbContext _context;

    public ProjectRepository(GridBaseDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        // 🔒 Hidden. Id + soft-delete filtresiyle tek proje.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<IReadOnlyList<Project>> GetByOwnerAsync(string ownerUserId, CancellationToken ct = default)
    {
        // 🔒 Hidden. owner + soft-delete → CreatedAt'e göre azalan sırala.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<int> CountByOwnerAsync(string ownerUserId, CancellationToken ct = default)
    {
        // 🔒 Hidden. owner + soft-delete filtresiyle sayım (Free plan: 2 proje sınırı buradan denetlenir).
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<bool> NameExistsForOwnerAsync(
        string name, string ownerUserId, long? excludeId = null, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: adı normalize et → owner + soft-delete + ad eşleşmesi
        //       → excludeId verilmişse o projeyi hariç tut (update senaryosu).
        throw new NotImplementedException("Source available on request.");
    }

    public async Task AddAsync(Project project, CancellationToken ct = default)
    {
        // 🔒 Hidden. Yeni projeyi context'e ekler.
        throw new NotImplementedException("Source available on request.");
    }

    public void Remove(Project project)
    {
        // 🔒 Hidden. Projeyi kaldırır (soft-delete interceptor'da çözülür).
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<int> CountTablesAsync(long projectId, CancellationToken ct = default)
    {
        // 🔒 Hidden. projectId + soft-delete filtresiyle tablo sayımı.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<int> CountRowsAsync(long projectId, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: projenin tablo id'lerini çek → o tablolara ait silinmemiş satırları say.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<IReadOnlyList<Datatable>> GetRecentTablesAsync(
        long projectId, int take, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: projectId + soft-delete → kolon/satır include
        //       → CreatedAt azalan → ilk N.
        throw new NotImplementedException("Source available on request.");
    }
}