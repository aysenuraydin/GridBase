using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableColumns.Commands.CreateBulkTableColumn;

public class CreateBulkTableColumnCommand : IRequest<Result<long>>
{
    public long TableId { get; set; }
    public List<TableColumnBulkCreateDto> Columns { get; set; } = new();
}
public class CreateBulkTableColumnCommandHandler : IRequestHandler<CreateBulkTableColumnCommand, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateBulkTableColumnCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<long>> Handle(CreateBulkTableColumnCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: boş ad kontrolü → batch içi tekrar kontrolü →
        //   tabloda mevcut ad çakışması kontrolü → her kolonu oluştur +
        //   tüm satırlara boş hücre aç → Result.
        throw new NotImplementedException("Source available on request.");
    }
}