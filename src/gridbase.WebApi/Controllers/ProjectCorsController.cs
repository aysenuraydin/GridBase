using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services;
using gridbase.DTO.DTOs;

namespace gridbase.WebApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/projects/{projectId:long}/cors")]
public class ProjectCorsController(IProjectCorsService corsService) : ControllerBase
{
    private readonly IProjectCorsService _service = corsService;

    [HttpGet]
    public async Task<IActionResult> Get(long projectId, CancellationToken ct)
        => Ok(await _service.GetByProjectAsync(projectId, ct));

    [HttpPost]
    public async Task<IActionResult> Add(long projectId, [FromBody] AddCorsOriginRequest request, CancellationToken ct)
        => Ok(await _service.AddAsync(projectId, request, ct));

    [HttpDelete("{originId:long}")]
    public async Task<IActionResult> Remove(long projectId, long originId, CancellationToken ct)
    {
        var ok = await _service.RemoveAsync(projectId, originId, ct);
        return ok ? NoContent() : NotFound();
    }
}