using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using gridbase.Application.Features.Schema.Queries.GetSchema;
using gridbase.Application.Features.Validation.Commands.SetColumnValidation;
using gridbase.Application.Features.Validation.Queries.GetColumnValidation;

namespace gridbase.WebApi.Controllers;

public partial class GridBaseController
{
    [HttpGet("{tableName}/schema")]
    public async Task<IActionResult> GetSchema(string tableName)
        => ToResult(await _mediator.Send(new GetSchemaQuery(tableName)));

    [HttpGet("{tableName}/columns/{columnName}/validation")]
    public async Task<IActionResult> GetColumnValidation(string tableName, string columnName)
        => ToResult(await _mediator.Send(new GetColumnValidationQuery(tableName, columnName)));

    [HttpPut("{tableName}/columns/{columnName}/validation")]
    [Authorize(Roles = "Admin,GB")]
    public async Task<IActionResult> SetColumnValidation(
        string tableName, string columnName, [FromBody] SetColumnValidationCommand command)
    {
        command.TableName = tableName;
        command.ColumnName = columnName;
        return ToResult(await _mediator.Send(command));
    }
}
