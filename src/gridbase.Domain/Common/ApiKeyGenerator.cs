using System.Security.Cryptography;
using System.Text;
using gridbase.Domain.Enums;

namespace gridbase.Application.Common.Security;

public static class ApiKeyGenerator
{
    public record GeneratedKey(string RawKey, string KeyHash, string KeyPrefix);

    public static GeneratedKey Generate(ApiKeyType type)
    {
        // 🔒 Implementation hidden — commercial product.
        // Akış: prefix seç (pk/sk) → kriptografik rastgele token → SHA256 hash → gösterim prefix'i
        throw new NotImplementedException("Source available on request.");
    }
    public static string Hash(string rawKey)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawKey));
        return Convert.ToHexString(bytes);  // buyuk harf hex string
    }

    public static ApiKeyType? DetectType(string rawKey)
    {
        if (rawKey.StartsWith("gb_sk_", StringComparison.Ordinal)) return ApiKeyType.Secret;
        if (rawKey.StartsWith("gb_pk_", StringComparison.Ordinal)) return ApiKeyType.Anon;
        return null;
    }

    private static string RandomToken(int byteLength)
    {
        var bytes = RandomNumberGenerator.GetBytes(byteLength);
        // URL-safe base64 (+ / = → temizle)
        return Convert.ToBase64String(bytes)
            .Replace("+", "").Replace("/", "").Replace("=", "");
    }
}