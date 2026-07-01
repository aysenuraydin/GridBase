using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.Domain.Entities;

namespace gridbase.Application.Features.Datatables.Queries.GetTableAccess;

public record GetTableAccessQuery(long Id) : IRequest<Result<TableAccessDto>>;
public class GetTableAccessQueryHandler : IRequestHandler<GetTableAccessQuery, Result<TableAccessDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetTableAccessQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<TableAccessDto>> Handle(GetTableAccessQuery request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: tabloyu no-tracking projeksiyon ile TableAccessDto'ya
        //   çek → yoksa Failure → Result.
        throw new NotImplementedException("Source available on request.");
    }
}