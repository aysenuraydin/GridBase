using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Features.Relations.Commands.AddRelation;
using gridbase.Application.Features.Relations.Queries.GetRelations;
using gridbase.Application.Features.Relations.Commands.RemoveRelation;

namespace gridbase.WebApi.Controllers;

public partial class GridBaseController
{
    [HttpGet("relations/{tableName}")]
    [Authorize(Roles = "Admin,GB")]
    public async Task<IActionResult> GetRelations(string tableName)
        => ToResult(await _mediator.Send(new GetRelationsQuery(tableName)));

    [HttpPost("relations/{tableName}")]
    [Authorize(Roles = "Admin,GB")]
    public async Task<IActionResult> AddRelation(string tableName, [FromBody] AddRelationCommand command)
    {
        command.FromTable = tableName;
        return ToResult(await _mediator.Send(command));
    }

    [HttpDelete("relations/{tableName}/{toTable}")]
    [Authorize(Roles = "Admin,GB")]
    public async Task<IActionResult> RemoveRelation(string tableName, string toTable)
        => ToResult(await _mediator.Send(new RemoveRelationCommand(tableName, toTable)));
}
