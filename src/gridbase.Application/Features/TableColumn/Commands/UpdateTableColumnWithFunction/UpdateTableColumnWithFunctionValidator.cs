
using FluentValidation;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithFunction;

namespace gridbase.Application.Features.TableColumns.Commands.UpdateTableColumn;

public class UpdateTableColumnWithFunctionValidator : AbstractValidator<UpdateTableColumnWithFunctionCommand>
{
    public UpdateTableColumnWithFunctionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Güncellenecek kolon ID'si boş olamaz.");
    }
}