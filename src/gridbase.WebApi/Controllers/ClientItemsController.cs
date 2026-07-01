using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "GB")]

public class ClientItemsController(IClientItemService clientItemService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<ClientItemDto>>> GetAll(CancellationToken ct)
        => Ok(await clientItemService.GetAllAsync(ct));

    [HttpPost]
    public async Task<ActionResult<ClientItemDto>> Create(ClientItemDto dto, CancellationToken ct)
    {
        var created = await clientItemService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetAll), new { }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClientItemDto>> Update(int id, ClientItemDto dto, CancellationToken ct)
    {
        try
        {
            return Ok(await clientItemService.UpdateAsync(id, dto, ct));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        try
        {
            await clientItemService.DeleteAsync(id, ct);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

