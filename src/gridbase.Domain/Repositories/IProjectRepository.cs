using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(long id, CancellationToken ct = default);

    // Bir kullanicinin tum projeleri (silinmemis)
    Task<IReadOnlyList<Project>> GetByOwnerAsync(string ownerUserId, CancellationToken ct = default);

    // Limit kontrolu icin: kullanicinin proje sayisi
    Task<int> CountByOwnerAsync(string ownerUserId, CancellationToken ct = default);

    // Ayni sahip altinda ayni isimde proje var mi? (excludeId: guncellemede kendini haric tut)
    Task<bool> NameExistsForOwnerAsync(string name, string ownerUserId, long? excludeId = null, CancellationToken ct = default);

    Task AddAsync(Project project, CancellationToken ct = default);
    void Remove(Project project);

    // Bu projeye ait tablo sayisi (ozet icin)
    Task<int> CountTablesAsync(long projectId, CancellationToken ct = default);


    // Bir projenin toplam satir sayisi (tum tablolar)
    Task<int> CountRowsAsync(long projectId, CancellationToken ct = default);

    // Son N tablo (id, ad, satir/kolon sayisi)
    Task<IReadOnlyList<Datatable>> GetRecentTablesAsync(long projectId, int take, CancellationToken ct = default);
}