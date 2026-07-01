using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Authorize(Roles = "GB")]
[Route("api/[controller]")]
public class AboutController(IAboutConfigService service) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<AboutConfigDto>> Get() =>
        Ok(await service.GetAsync());

    [HttpPut]
    public async Task<ActionResult<AboutConfigDto>> Upsert(AboutConfigDto dto) =>
        Ok(await service.UpsertAsync(dto));
}














