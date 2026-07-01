using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Domain.Common;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithValidation;

public class UpdateTableColumnWithValidationCommand : IRequest<Result<bool>>
{
    public long Id { get; set; }
    public ColumnValidationConfigDto ValidationFk { get; set; } = new();
}
public class UpdateTableColumnWithValidationCommandHandler : IRequestHandler<UpdateTableColumnWithValidationCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateTableColumnWithValidationCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(UpdateTableColumnWithValidationCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: kolonu validation+rules ile bul → yoksa NotFound →
        //   gelen kuralları domain kurallarına çevir → UpdateValidation(type, rules) → Result.
        throw new NotImplementedException("Source available on request.");
    }
}