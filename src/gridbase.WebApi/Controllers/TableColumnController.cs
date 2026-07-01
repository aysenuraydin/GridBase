using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Common.Models;
using gridbase.Application.Features.TableColumns.Commands.CreateBulkTableColumn;
using gridbase.Application.Features.TableColumns.Commands.CreateTableColumn;
using gridbase.Application.Features.TableColumns.Commands.DeleteColumn;
using gridbase.Application.Features.TableColumns.Commands.RestoreBulkTableColumn;
using gridbase.Application.Features.TableColumns.Commands.RestoreTableColumn;
using gridbase.Application.Features.TableColumns.Commands.UpdateBulkTableColumnWithDesign;
using gridbase.Application.Features.TableColumns.Commands.UpdateBulkTableColumnWithFunction;
using gridbase.Application.Features.TableColumns.Commands.UpdateBulkTableColumnWithModal;
using gridbase.Application.Features.TableColumns.Commands.UpdateBulkTableColumnWithOption;
using gridbase.Application.Features.TableColumns.Commands.UpdateBulkTableColumnWithValidation;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumn;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithDesign;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithFunction;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithModal;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithOption;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithValidation;
using gridbase.Application.Features.TableColumns.Queries.GetDeletedTableColumnTableById;
using gridbase.Application.Features.TableColumns.Queries.GetTableColumnTableById;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Entities;

namespace gridbase.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "GB, User")]
public class TableColumnController : BaseController<TableColumn, long>
{
    private readonly IMediator _mediator;
    public TableColumnController(ITableColumnService service, IMediator mediator) : base(service)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTableColumns()
        => Ok(await _mediator.Send(new GetDatatableColumnsQuery()));

    [HttpGet("datatable/{tableId}")]
    public async Task<IActionResult> GetAllTableColumnByDatatableId(long tableId)
        => Ok(await _mediator.Send(new GetDatatableColumnsByTableIdQuery(tableId)));

    [HttpGet("table/{tableId}")]
    public async Task<IActionResult> GetAllTableColumnByTableId(long tableId)
        => Ok(await _mediator.Send(new GetTableColumnsByTableIdQuery(tableId)));

    [HttpGet("deleted/{tableId}")]
    public async Task<IActionResult> GetAllDeletedTableColumnByTableId(long tableId)
        => Ok(await _mediator.Send(new GetDeletedTableColumnsByTableIdQuery(tableId)));

    [HttpPost]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> CreateTableColumn(CreateTableColumnCommand command)
        => Ok(await _mediator.Send(command));


    [HttpPut("{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateTableColumn(long id, UpdateTableColumnCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("design/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateTableColumnWithDesign(long id, UpdateTableColumnWithDesignCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("option/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateTableColumnWithOption(long id, UpdateTableColumnWithOptionCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("validation/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateTableColumnWithValidation(long id, UpdateTableColumnWithValidationCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("modal/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateTableColumnWithModal(long id, UpdateTableColumnWithModalDesignCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpPut("function/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateTableColumnWithFunction(long id, UpdateTableColumnWithFunctionCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("restore/{colId}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> RestoreTableColumn(long colId)
        => Ok(await _mediator.Send(new RestoreTableColumnCommand(colId)));

    [HttpDelete("{colId}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> DeleteTableColumn(long colId)
        => Ok(await _mediator.Send(new DeleteTableColumnCommand(colId)));

    [HttpDelete("hardDelete/{colId}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> HardDeleteTableColumn(long colId)
        => Ok(await _mediator.Send(new HardDeleteTableColumnCommand(colId)));




    [HttpPost("bulk/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> CreateBulkTableColumn(long id, [FromBody] CreateBulkTableColumnCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }


    [HttpPut("bulk/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateBulkTableColumn(long id, [FromBody] UpdateBulkTableColumnCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
        // return NotFound(result);
        // return NoContent();
    }
    [HttpPut("bulkDesign/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateBulkTableColumnWithDesign(long id, [FromBody] UpdateBulkTableColumnWithDesignCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("bulkOption/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateBulkTableColumnWithOption(long id, [FromBody] UpdateBulkTableColumnWithOptionCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("bulkValidation/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateBulkTableColumnWithValidation(long id, [FromBody] UpdateBulkTableColumnWithValidationCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpPut("bulkModal/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateBulkTableColumnWithModal(long id, [FromBody] UpdateBulkTableColumnWithModalDesignCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpPut("bulkFunction/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> UpdateBulkTableColumnWithFunction(long id, [FromBody] UpdateBulkTableColumnWithFunctionCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("bulkRestore/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> RestoreBulkTableColumn(long id, [FromBody] RestoreBulkTableColumnCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("bulk/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> DeleteBulkTableColumn(long id, [FromBody] DeleteBulkTableColumnCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("bulkHardDelete/{id}")]
    [Authorize(Roles = "GB")]
    public async Task<IActionResult> HardDeleteBulkTableColumn(long id, [FromBody] HardDeleteBulkTableColumnCommand command)
    {
        if (id != command.TableId)
            return BadRequest(Result<bool>.Failure("URL'deki ID ile gövdedeki ID uyuşmuyor!"));

        var result = await _mediator.Send(command);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
}

























