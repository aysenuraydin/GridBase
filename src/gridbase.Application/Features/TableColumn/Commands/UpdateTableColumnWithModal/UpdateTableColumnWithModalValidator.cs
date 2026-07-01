
using FluentValidation;
using gridbase.Application.Features.TableColumns.Commands.UpdateTableColumnWithModal;

namespace gridbase.Application.Features.TableColumns.Commands.UpdateTableColumn;

public class UpdateTableColumnWithModalValidator : AbstractValidator<UpdateTableColumnWithModalDesignCommand>
{
    public UpdateTableColumnWithModalValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Güncellenecek table ID'si boş olamaz.");
    }
}