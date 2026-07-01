using gridbase.Application.Common.Interfaces;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;
using gridbase.Domain.Repositories;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Services;

public class ApiKeyService : IApiKeyService
{
    private readonly IApiKeyRepository _apiKeyRepo;
    private readonly IProjectRepository _projectRepo;
    private readonly IUnitOfWork _uow;
    private readonly IUser _currentUser;

    public ApiKeyService(
        IApiKeyRepository apiKeyRepo,
        IProjectRepository projectRepo,
        IUnitOfWork uow,
        IUser currentUser)
    {
        _apiKeyRepo = apiKeyRepo;
        _projectRepo = projectRepo;
        _uow = uow;
        _currentUser = currentUser;
    }

    private bool IsAdmin =>
        string.Equals(_currentUser.Role, "Admin", StringComparison.OrdinalIgnoreCase)
        || string.Equals(_currentUser.Role, "GB", StringComparison.OrdinalIgnoreCase);

    private async Task<Project> EnsureOwnedProjectAsync(long projectId, CancellationToken ct)
    {
        // 🔒 Hidden. Akış: projeyi getir → yoksa NotFound →
        //   Admin değilse ve sahibi değilse Unauthorized.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<IReadOnlyList<ApiKeyListItem>> GetByProjectAsync(long projectId, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: sahiplik doğrula → anahtarları çek → liste DTO'suna map.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<CreatedApiKeyResponse> CreateAsync(
        long projectId, CreateApiKeyRequest request, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: sahiplik doğrula → kriptografik anahtar üret (hash + prefix)
        //   → ApiKey.Create → kaydet → ham anahtarı tek seferlik döndür.
        throw new NotImplementedException("Source available on request.");
    }

    public async Task<bool> RevokeAsync(long projectId, long keyId, CancellationToken ct = default)
    {
        // 🔒 Hidden. Akış: sahiplik doğrula → anahtarı bul/projeye ait mi → Revoke → kaydet.
        throw new NotImplementedException("Source available on request.");
    }
}