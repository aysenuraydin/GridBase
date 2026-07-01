
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController(IStatsSectionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<StatsSectionDto>> Get() =>
        Ok(await service.GetAsync());

    [HttpPut]
    [Authorize(Roles = "GB")]
    public async Task<ActionResult<StatsSectionDto>> Upsert(StatsSectionDto dto) =>
        Ok(await service.UpsertAsync(dto));
}
