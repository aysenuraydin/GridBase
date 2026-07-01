using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Application.Services.Interfaces;

namespace gridbase.Application.Features.Rows.Commands.CreateRow;

public class CreateRowCommand : IRequest<Result<IDictionary<string, object?>>>
{
    public string TableName { get; set; } = null!;
    public Dictionary<string, object?> Body { get; set; } = new();

    public CreateRowCommand(string tableName, Dictionary<string, object?> body)
    {
        TableName = tableName;
        Body = body;
    }
}
public class CreateRowCommandHandler : IRequestHandler<CreateRowCommand, Result<IDictionary<string, object?>>>
{
    private readonly IGridBaseService _service;
    public CreateRowCommandHandler(IGridBaseService service) => _service = service;

    public async Task<Result<IDictionary<string, object?>>> Handle(CreateRowCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: motor servisinde tabloya gövdeyle satır oluştur → Success.
        throw new NotImplementedException("Source available on request.");
    }
}