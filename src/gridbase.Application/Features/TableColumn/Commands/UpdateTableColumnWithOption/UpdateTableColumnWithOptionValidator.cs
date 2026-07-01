
using FluentValidation;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithOption;

namespace gridbase.Application.Features.TableColumns.Commands.UpdateTableColumn;

public class UpdateTableColumnWithOptionValidator : AbstractValidator<UpdateTableColumnWithOptionCommand>
{
    public UpdateTableColumnWithOptionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Güncellenecek kolon ID'si boş olamaz.");
    }
}