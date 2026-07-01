using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Common.Models;
using gridbase.Application.Features.Datatables.Commands.ChangeTableHeight;
using gridbase.Application.Features.Datatables.Commands.CreateDatatable;
using gridbase.Application.Features.Datatables.Commands.DeleteBulkDatatable;
using gridbase.Application.Features.Datatables.Commands.DeleteDatatable;
using gridbase.Application.Features.Datatables.Commands.HardDeleteBulkDatatable;
using gridbase.Application.Features.Datatables.Commands.HardDeleteDatatable;
using gridbase.Application.Features.Datatables.Commands.RestoreDeletedDatatable;
using gridbase.Application.Features.Datatables.Commands.SetTableAccess;
using gridbase.Application.Features.Datatables.Commands.UpdateDatatable;
using gridbase.Application.Features.Datatables.Commands.UpdateForeignTable;
using gridbase.Application.Features.Datatables.Queries.GetTableAccess;
using gridbase.Application.Features.Datatables.Queries.GetTables;
using gridbase.Application.Features.Tables.Queries.GetTableById;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Entities;

namespace gridbase.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "GB, User")]
public class DatatableController : BaseController<Datatable, long>
{
    private readonly IMediator _mediator;
    public DatatableController(IDatatableService service, IMediator mediator) : base(service)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDatatables()
        => Ok(await _mediator.Send(new GetDatatablesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllDatatableById(long id)
        => Ok(await _mediator.Send(new GetDatatableByIdQuery(id)));

    [HttpGet("table/{tableId}")]
    public async Task<IActionResult> GetAllDatatableByTableId(int tableId)
        => Ok(await _mediator.Send(new GetTableByIdQuery(tableId)));

    [HttpGet("deleted")]
    public async Task<IActionResult> GetAllDeletedDatatable()
        => Ok(await _mediator.Send(new GetDeletedDatatablesQuery()));

    [HttpGet("relationships")]
    public async Task<IActionResult> GetAllDatatablesWithRelationships()
        => Ok(await _mediator.Send(new GetDatatablesWithRelationshipsQuery()));

    [HttpPost]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> CreateDatatable(CreateDatatableCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateDatatable(long id, UpdateDatatableCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("updateForeignTable/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> updateForeignTable(long id, UpdateForeignTableCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("changeTableHeight/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> ChangeTableHeight(long id, ChangeTableHeightCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("restore/{tableId}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> RestoreDatatable(long tableId)
        => Ok(await _mediator.Send(new RestoreDeletedDatatableCommand(tableId)));

    [HttpDelete("{tableId}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> DeleteDatatable(long tableId)
        => Ok(await _mediator.Send(new DeleteDatatableCommand(tableId)));

    [HttpDelete("hardDelete/{tableId}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> HardDeleteDatatable(long tableId)
        => Ok(await _mediator.Send(new HardDeleteDatatableCommand(tableId)));

    [HttpDelete("bulk")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> DeleteBulkDatatables([FromBody] DeleteBulkDatatableCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("bulkHardDelete")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> HardDeleteBulkDatatables([FromBody] HardDeleteBulkDatatableCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpGet("getAccess/{id}")]
    public async Task<IActionResult> GetTableAccess(long id)
    {
        var result = await _mediator.Send(new GetTableAccessQuery(id));
        return result.Succeeded ? Ok(result) : NotFound(result);
    }


    [HttpPut("setAccess/{id}")]
    [Authorize(Roles = "GB,Admin")]
    public async Task<IActionResult> SetTableAccess(long id, SetTableAccessCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
}




















