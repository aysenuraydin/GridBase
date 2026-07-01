using Microsoft.Extensions.Caching.Memory;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;

namespace gridbase.Application.Services;

public class TenantConfigService : ITenantConfigService
{
    private readonly ITenantConfigRepository _repository;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "tenant:config:default";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

    public TenantConfigService(ITenantConfigRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<TenantConfig> GetConfigAsync(CancellationToken ct = default)
    {
        // 🔒 Hidden. cache hit → döndür; miss → DB → yoksa default oluştur → cache'le.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<TenantConfig> UpdateConfigAsync(TenantConfig incoming, CancellationToken ct = default)
    {
        // 🔒 Hidden. mevcut yoksa ekle; varsa audit koruyarak alanları kopyala → kaydet → cache'le.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<TenantConfig> ResetToDefaultAsync(CancellationToken ct = default)
    {
        // 🔒 Hidden. varsayılan değerleri uygula → kaydet → cache'le.
        throw new NotImplementedException("Source available on request.");
    }

    public void InvalidateCache() => _cache.Remove(CacheKey);
}