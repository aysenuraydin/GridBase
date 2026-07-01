using gridbase.Domain.Entities;

namespace gridbase.Domain.Repositories;

public interface IApiKeyRepository
{
    // Hash ile bul (gelen istegi dogrulamak icin — EN KRITIK metot)
    Task<ApiKey?> GetByHashAsync(string keyHash, CancellationToken ct = default);

    // Bir projenin tum key'leri (liste ekrani)
    Task<IReadOnlyList<ApiKey>> GetByProjectAsync(long projectId, CancellationToken ct = default);

    Task<ApiKey?> GetByIdAsync(long id, CancellationToken ct = default);

    Task AddAsync(ApiKey apiKey, CancellationToken ct = default);
    void Remove(ApiKey apiKey);
}