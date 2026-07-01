using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using gridbase.Application.Common.Security;
using gridbase.Domain.Enums;
using gridbase.Domain.Repositories;

namespace gridbase.WebApi.Auth;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions { }

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    public const string SchemeName = "ApiKey";
    public const string HeaderName = "X-GridBase-Key";

    public const string ClaimProjectId = "gb_project_id";
    public const string ClaimKeyType = "gb_key_type";
    public const string ClaimKeyId = "gb_key_id";

    private readonly IApiKeyRepository _apiKeyRepository;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IApiKeyRepository apiKeyRepository)
        : base(options, logger, encoder)
    {
        _apiKeyRepository = apiKeyRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // 1) Header var mi?
        if (!Request.Headers.TryGetValue(HeaderName, out var raw))
            return AuthenticateResult.NoResult();   // bu sema bu istegi ilgilendirmiyor

        var rawKey = raw.ToString().Trim();
        if (string.IsNullOrEmpty(rawKey))
            return AuthenticateResult.NoResult();

        // 2) Hash'le
        var hash = ApiKeyGenerator.Hash(rawKey);

        // 3) DB'de bul
        var apiKey = await _apiKeyRepository.GetByHashAsync(hash, Context.RequestAborted);
        if (apiKey is null || !apiKey.IsActive)
            return AuthenticateResult.Fail("Gecersiz ya da iptal edilmis API key.");

        // (opsiyonel) son kullanim — performans icin simdilik yazmiyoruz,
        // istenirse apiKey.MarkUsed() + commit (ayri scope) eklenir.

        // 4) Kimlik uret — claim'ler
        var claims = new List<Claim>
        {
            new(ApiKeyAuthenticationHandler.ClaimProjectId, apiKey.ProjectId.ToString()),
            new(ApiKeyAuthenticationHandler.ClaimKeyType, apiKey.KeyType.ToString()),
            new(ApiKeyAuthenticationHandler.ClaimKeyId, apiKey.Id.ToString()),
            // secretKey ise "service" rolu (bypass) — JWT'deki Role gibi
            new(ClaimTypes.Role, apiKey.KeyType == ApiKeyType.Secret ? "GB" : "ApiAnon"),
        };

        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        return AuthenticateResult.Success(ticket);
    }
}