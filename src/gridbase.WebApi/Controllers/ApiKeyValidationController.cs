using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Domain.Repositories;

namespace gridbase.WebApi.Controllers;

[ApiController]
[Route("api/apikeys")]
[AllowAnonymous]
public class ApiKeyValidationController : ControllerBase
{
    private readonly IApiKeyRepository _apiKeyRepository;
    private readonly IConfiguration _config;

    public ApiKeyValidationController(
        IApiKeyRepository apiKeyRepository,
        IConfiguration config)
    {
        _apiKeyRepository = apiKeyRepository;
        _config = config;
    }
    [HttpPost("validate")]
    public async Task<IActionResult> Validate(
        [FromBody] ValidateKeyRequest body,
        CancellationToken ct)
    {
        throw new NotImplementedException("Source available on request.");
    }
}

public class ValidateKeyRequest
{
    public string Key { get; set; } = "";
}