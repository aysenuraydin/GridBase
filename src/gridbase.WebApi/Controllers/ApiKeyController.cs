using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services;
using gridbase.DTO.DTOs;

namespace gridbase.WebApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/projects/{projectId:long}/keys")]
public class ApiKeyController(IApiKeyService apiKeyService) : ControllerBase
{
    private readonly IApiKeyService _service = apiKeyService;

    [HttpGet]
    public async Task<IActionResult> GetKeys(long projectId, CancellationToken ct)
    {
        var keys = await _service.GetByProjectAsync(projectId, ct);
        return Ok(keys);
    }

    [HttpPost]
    public async Task<IActionResult> Create(long projectId, [FromBody] CreateApiKeyRequest request, CancellationToken ct)
    {
        var created = await _service.CreateAsync(projectId, request, ct);
        return Ok(created);
    }

    [HttpDelete("{keyId:long}")]
    public async Task<IActionResult> Revoke(long projectId, long keyId, CancellationToken ct)
    {
        var ok = await _service.RevokeAsync(projectId, keyId, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}