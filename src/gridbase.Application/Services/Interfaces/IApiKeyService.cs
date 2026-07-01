using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public interface IApiKeyService
{
    Task<IReadOnlyList<ApiKeyListItem>> GetByProjectAsync(long projectId, CancellationToken ct = default);
    Task<CreatedApiKeyResponse> CreateAsync(long projectId, CreateApiKeyRequest request, CancellationToken ct = default);
    Task<bool> RevokeAsync(long projectId, long keyId, CancellationToken ct = default);
}