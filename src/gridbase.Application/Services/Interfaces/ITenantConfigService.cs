using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Services.Interfaces;

public interface ITenantConfigService
{
    Task<TenantConfig> GetConfigAsync(CancellationToken ct = default);
    Task<TenantConfig> UpdateConfigAsync(TenantConfig incoming, CancellationToken ct = default);
    Task<TenantConfig> ResetToDefaultAsync(CancellationToken ct = default);
    void InvalidateCache();
}

