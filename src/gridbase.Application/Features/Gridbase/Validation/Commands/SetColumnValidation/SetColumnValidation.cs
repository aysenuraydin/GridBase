using MediatR;
using gridbase.Application.Common.Models;
using gridbase.Application.Services.Interfaces;
using gridbase.Domain.Enums;
using gridbase.DTO.DTOs;

namespace gridbase.Application.Features.Validation.Commands.SetColumnValidation;

public class SetColumnValidationCommand : IRequest<Result<bool>>
{
    public string TableName { get; set; } = null!;
    public string ColumnName { get; set; } = null!;
    public FieldTypeEnum Type { get; set; }
    public List<ValidationRuleDto> Rules { get; set; } = new();
}
public class SetColumnValidationCommandHandler : IRequestHandler<SetColumnValidationCommand, Result<bool>>
{
    private readonly IGridBaseService _service;
    public SetColumnValidationCommandHandler(IGridBaseService service) => _service = service;

    public async Task<Result<bool>> Handle(SetColumnValidationCommand request, CancellationToken cancellationToken)
    {
        // 🔒 Hidden. Akış: request → SetColumnValidationRequest DTO → motor servisinde
        //   kolonun doğrulama kurallarını uygula → Success.
        throw new NotImplementedException("Source available on request.");
    }
}