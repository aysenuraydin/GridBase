using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Common.Models;
using gridbase.Application.Features.Datatables.Commands.RestoreDeletedMenuItem;
using gridbase.Application.Features.MenuItems.Commands.ChangePrivacyMenuItem;
using gridbase.Application.Features.MenuItems.Commands.CreateDivider;
using gridbase.Application.Features.MenuItems.Commands.CreateMenuItem;
using gridbase.Application.Features.MenuItems.Commands.DeleteMenuItem;
using gridbase.Application.Features.MenuItems.Commands.HardDeleteMenuItem;
using gridbase.Application.Features.MenuItems.Commands.ShowOrHideMenuItem;
using gridbase.Application.Features.MenuItems.Commands.UpdateDivider;
using gridbase.Application.Features.MenuItems.Commands.UpdateMenuItem;
using gridbase.Application.Features.MenuItems.Commands.updateMenuItemOrder;
using gridbase.Application.Features.MenuItems.Queries.GetDeletedMenuItems;
using gridbase.Application.Features.MenuItems.Queries.GetMenuItemById;
using gridbase.Application.Features.MenuItems.Queries.GetMenuItems;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Entities;

namespace gridbase.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "GB")]
public class MenuItemController : BaseController<MenuItem, long>
{
    private readonly IMediator _mediator;
    public MenuItemController(IMenuItemService service, IMediator mediator) : base(service)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllMenuItems()
        => Ok(await _mediator.Send(new GetMenuItemsQuery()));

    [HttpGet("deleted")]
    public async Task<IActionResult> GetAllDeletedMenuItems()
        => Ok(await _mediator.Send(new GetDeletedMenuItemsQuery()));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetMenuItemById(long id)
        => Ok(await _mediator.Send(new GetMenuItemByIdQuery(id)));

    [HttpPost("item")]
    public async Task<IActionResult> CreateItem(CreateMenuItemCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("divider")]
    public async Task<IActionResult> CreateDivider(CreateDividerCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPut("item/{id}")]
    public async Task<IActionResult> UpdateItem(long id, UpdateMenuItemCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("divider/{id}")]
    public async Task<IActionResult> UpdateDivider(long id, UpdateDividerCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("showOrHide/{id}")]
    public async Task<IActionResult> ShowOrHide(long id, ShowOrHideMenuItemCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("changePrivacy/{id}")]
    public async Task<IActionResult> ChangePrivacyMenuItem(long id, ChangePrivacyMenuItemCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpPut("changeOrder/{id}")]
    public async Task<IActionResult> ChangeOrder(long id, updateMenuItemOrderCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(long id)
        => Ok(await _mediator.Send(new DeleteMenuItemCommand(id)));

    [HttpDelete("hardDelete/{id}")]
    public async Task<IActionResult> HardDeleteMenuItem(long id)
        => Ok(await _mediator.Send(new HardDeleteMenuItemCommand(id)));

    [HttpDelete("restore/{id}")]
    public async Task<IActionResult> RestoreMenuItem(long id)
        => Ok(await _mediator.Send(new RestoreDeletedMenuItemCommand(id)));

}