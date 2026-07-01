using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LandingHeroController : ControllerBase
{
    private readonly ILandingHeroService _service;

    public LandingHeroController(ILandingHeroService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetConfig()
    {
        var result = await _service.GetConfigAsync();
        return result is null
            ? NotFound("Hero ayarı bulunamadı.")
            : Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateConfig([FromBody] UpdateLandingHeroDto dto)
    {
        await _service.UpdateConfigAsync(dto);
        return Ok(new { message = "Hero ayarları başarıyla güncellendi." });
    }
}


