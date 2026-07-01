using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.DTO.DTOs;
using gridbase.Application.Features.Tables.Commands.CreateTable;
using gridbase.Application.Features.Tables.Commands.UpdateTable;
using gridbase.Application.Features.Tables.Commands.DeleteTable;
using gridbase.Application.Features.Tables.Queries.GetAllTables;
using gridbase.Application.Features.Tables.Queries.GetOneTable;
using gridbase.Application.Features.Tables.Queries.GetTblById;
using gridbase.Application.Services.Interfaces;

namespace gridbase.WebApi.Controllers;

// Tablo (datatable) CRUD. Okuma serbest; yazma işlemleri Admin/GB rolü ister.
public partial class GridBaseController
{
    [HttpGet("tables")]
    public async Task<IActionResult> GetAllTables(
        [FromQuery(Name = "filter")] string[]? filter = null,
        [FromQuery] string? sort = null,
        [FromQuery] string? select = null)
    {
        var filters = filter?.Select(FilterDescriptor.TryParse).Where(f => f is not null).Cast<FilterDescriptor>().ToList() ?? [];
        var q = new GetAllTablesQuery(filters, SortDescriptor.TryParse(sort), SelectDescriptor.TryParse(select));
        return ToResult(await _mediator.Send(q));
    }

    [HttpGet("tables/one")]
    public async Task<IActionResult> GetOneTable(
        [FromQuery(Name = "filter")] string[]? filter = null,
        [FromQuery] string? sort = null,
        [FromQuery] string? select = null)
    {
        var filters = filter?.Select(FilterDescriptor.TryParse).Where(f => f is not null).Cast<FilterDescriptor>().ToList() ?? [];
        var q = new GetOneTableQuery(filters, SortDescriptor.TryParse(sort), SelectDescriptor.TryParse(select));
        return ToResult(await _mediator.Send(q));
    }

    [HttpGet("tables/{id:long}")]
    public async Task<IActionResult> GetTableById(long id)
        => ToResult(await _mediator.Send(new GetTblByIdQuery(id)));

    [HttpPost("tables")]
    [Authorize(Roles = "Admin,GB")]
    public async Task<IActionResult> CreateTable([FromBody] CreateTableCommand command)
        => ToResult(await _mediator.Send(command));

    [HttpPut("tables/{id:long}")]
    [Authorize(Roles = "Admin,GB")]
    public async Task<IActionResult> UpdateTable(long id, [FromBody] UpdateTableCommand command)
    {
        command.Id = id;
        return ToResult(await _mediator.Send(command));
    }

    [HttpDelete("tables/{id:long}")]
    [Authorize(Roles = "Admin,GB")]
    public async Task<IActionResult> DeleteTable(long id, [FromQuery] bool hard = true)
        => ToResult(await _mediator.Send(new DeleteTableCommand(id, hard)));
}
