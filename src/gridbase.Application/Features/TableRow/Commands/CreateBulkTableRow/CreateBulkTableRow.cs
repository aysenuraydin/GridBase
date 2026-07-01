using MediatR;
using gridbase.Application.Common.Behaviors;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableRows.Commands.CreateBulkTableRow;

public record CreateBulkTableRowCommand(long TableId, List<CreateBulkTableRowDto> Rows)
    : IRequest<Result<long>>, ITableScopedRequest
{
    public TableAccessType AccessType => TableAccessType.Write;
    public long? TableIdHint => TableId;
}
public class CreateBulkTableRowCommandHandler : IRequestHandler<CreateBulkTableRowCommand, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateBulkTableRowCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<long>> Handle(CreateBulkTableRowCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: her satır için TableRow.Create + hücrelerini oluştur → Result.
        throw new NotImplementedException("Source available on request.");
    }
}