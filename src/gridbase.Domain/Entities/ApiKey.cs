using gridbase.Domain.Common;
using gridbase.Domain.Enums;

namespace gridbase.Domain.Entities;

public class ApiKey : BaseAuditableEntity<long>
{
    public long ProjectId { get; private set; }
    public Project ProjectFk { get; private set; } = null!;

    public ApiKeyType KeyType { get; private set; }

    public string KeyHash { get; private set; } = null!;

    public string KeyPrefix { get; private set; } = null!;

    public string? Name { get; private set; }
    public DateTime? LastUsedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public bool IsActive => RevokedAt == null && DeletedAt == null;

    public ApiKey() { }

    public static ApiKey Create(
        long projectId,
        ApiKeyType keyType,
        string keyHash,
        string keyPrefix,
        string? name = null)
    {
        if (projectId <= 0)
            throw new DomainException("Gecerli bir proje gerekli.");
        if (string.IsNullOrWhiteSpace(keyHash))
            throw new DomainException("Key hash zorunlu.");

        return new ApiKey
        {
            ProjectId = projectId,
            KeyType = keyType,
            KeyHash = keyHash,
            KeyPrefix = keyPrefix,
            Name = name?.Trim()
        };
    }

    public void MarkUsed()
    {
        LastUsedAt = DateTime.UtcNow;
    }

    public void Revoke()
    {
        if (RevokedAt == null)
            RevokedAt = DateTime.UtcNow;
    }

    public void Rename(string? name)
    {
        Name = name?.Trim();
    }

    public void HardDelete()
    {
        IsHardDelete = true;
    }
}