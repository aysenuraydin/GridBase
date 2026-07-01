using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;

namespace gridbase.Application.Features.Datatables.Commands.RestoreDeletedDatatable;

public record RestoreDeletedDatatableCommand(long Id) : IRequest<Result<bool>>;
public class RestoreDeletedDatatableCommandHandler : IRequestHandler<RestoreDeletedDatatableCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    public RestoreDeletedDatatableCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(RestoreDeletedDatatableCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: tabloyu ilişkileriyle bul → yoksa NotFound → Restore → Result.
        throw new NotImplementedException("Source available on request.");
    }
}