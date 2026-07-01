using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Application.Services.Interfaces;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.Schema.Queries.GetSchema;

public record GetSchemaQuery(string TableName) : IRequest<Result<TableSchemaResponse>>;
public class GetSchemaQueryHandler : IRequestHandler<GetSchemaQuery, Result<TableSchemaResponse>>
{
    private readonly IGridBaseService _service;
    public GetSchemaQueryHandler(IGridBaseService service) => _service = service;

    public async Task<Result<TableSchemaResponse>> Handle(GetSchemaQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: motor servisinden tablo şemasını getir → Success.
        throw new NotImplementedException("Source available on request.");
    }
}