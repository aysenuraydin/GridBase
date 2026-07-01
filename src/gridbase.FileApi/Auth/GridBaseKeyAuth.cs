using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace gridbase.FileApi.Auth;

public class GridBaseKeyAuthOptions : AuthenticationSchemeOptions { }

public class GridBaseKeyAuthHandler : AuthenticationHandler<GridBaseKeyAuthOptions>
{
    public const string SchemeName = "GridBaseKey";
    public const string HeaderName = "X-GridBase-Key";

    public const string ClaimProjectId = "gb_project_id";
    public const string ClaimKeyType = "gb_key_type";

    private readonly IHttpClientFactory _httpFactory;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _config;

    public GridBaseKeyAuthHandler(
        IOptionsMonitor<GridBaseKeyAuthOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHttpClientFactory httpFactory,
        IMemoryCache cache,
        IConfiguration config)
        : base(options, logger, encoder)
    {
        _httpFactory = httpFactory;
        _cache = cache;
        _config = config;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // 1) Header var mı?
        if (!Request.Headers.TryGetValue(HeaderName, out var raw))
            return AuthenticateResult.NoResult();   // bu şema bu isteği ilgilendirmiyor

        var rawKey = raw.ToString().Trim();
        if (string.IsNullOrEmpty(rawKey))
            return AuthenticateResult.NoResult();

        // 2) Cache'te var mı? (key → doğrulama sonucu, 5 dk)
        var cacheKey = $"gbkey:{rawKey}";
        if (_cache.TryGetValue<KeyValidationResult>(cacheKey, out var cached) && cached is not null)
        {
            return BuildSuccess(cached);
        }

        // 3) GridBase'e sor
        KeyValidationResult? result;
        try
        {
            var client = _httpFactory.CreateClient("GridBase");
            var internalSecret = _config["Internal:ServiceSecret"];

            using var req = new HttpRequestMessage(HttpMethod.Post, "/api/apikeys/validate");
            req.Headers.Add("X-Internal-Secret", internalSecret);
            req.Content = JsonContent.Create(new { key = rawKey });

            using var resp = await client.SendAsync(req, Context.RequestAborted);

            if (!resp.IsSuccessStatusCode)
                return AuthenticateResult.Fail("Geçersiz ya da iptal edilmiş API key.");

            result = await resp.Content.ReadFromJsonAsync<KeyValidationResult>(
                cancellationToken: Context.RequestAborted);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "GridBase key doğrulama çağrısı başarısız.");
            return AuthenticateResult.Fail("Key doğrulama servisi erişilemez.");
        }

        if (result is null || !result.Valid)
            return AuthenticateResult.Fail("Geçersiz API key.");

        // 4) Cache'le (5 dk) + başarı
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return BuildSuccess(result);
    }

    private AuthenticateResult BuildSuccess(KeyValidationResult r)
    {
        var claims = new List<Claim>
        {
            new(ClaimProjectId, r.ProjectId.ToString()),
            new(ClaimKeyType, r.KeyType ?? "Anon"),
            // secret ise bypass rolü, anon ise sınırlı
            new(ClaimTypes.Role, r.KeyType == "Secret" ? "GB" : "ApiAnon"),
        };

        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);
        return AuthenticateResult.Success(ticket);
    }

    private class KeyValidationResult
    {
        public bool Valid { get; set; }
        public long ProjectId { get; set; }
        public string? KeyType { get; set; }
        public long KeyId { get; set; }
    }
}