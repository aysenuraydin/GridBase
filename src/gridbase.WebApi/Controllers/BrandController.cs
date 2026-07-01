using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController(IBrandService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<BrandConfigDto>> Get() =>
        Ok(await service.GetAsync());

    [HttpPut]
    [Authorize(Roles = "GB")]
    public async Task<ActionResult<BrandConfigDto>> Upsert(BrandConfigDto dto) =>
        Ok(await service.UpsertAsync(dto));
}
