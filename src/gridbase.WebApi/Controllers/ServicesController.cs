using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(IServiceSectionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceSectionDto>> Get() =>
        Ok(await service.GetAsync());

    [HttpPut]
    [Authorize(Roles = "GB")]
    public async Task<ActionResult<ServiceSectionDto>> Upsert(ServiceSectionDto dto) =>
        Ok(await service.UpsertAsync(dto));
}
