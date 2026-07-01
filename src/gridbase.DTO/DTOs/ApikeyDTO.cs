using gridbase.Domain.Enums;

namespace gridbase.DTO.DTOs;

public sealed class CreateApiKeyRequest
{
    public ApiKeyType KeyType { get; set; }
    public string? Name { get; set; }
}

public sealed class CreatedApiKeyResponse
{
    public long Id { get; set; }
    public ApiKeyType KeyType { get; set; }
    public string RawKey { get; set; } = null!;
    public string KeyPrefix { get; set; } = null!;
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class ApiKeyListItem
{
    public long Id { get; set; }
    public ApiKeyType KeyType { get; set; }
    public string KeyPrefix { get; set; } = null!;
    public string? Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public bool IsActive { get; set; }
}