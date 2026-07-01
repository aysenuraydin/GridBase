using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaqController(IFaqService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<FaqCategoryDto>>> GetAll() =>
        Ok(await service.GetAllAsync());

    [HttpPut]
    [Authorize(Roles = "GB")]
    public async Task<ActionResult<List<FaqCategoryDto>>> Upsert(List<FaqCategoryDto> dto) =>
        Ok(await service.UpsertAsync(dto));
}
