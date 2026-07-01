
using FluentValidation;
using gridbase.Application.Features.TableColumns.Commands.UpdateBulkTableColumnWithModal;

namespace gridbase.Application.Features.TableColumns.Commands.UpdateBulkTableColumn;

public class UpdateBulkTableColumnWithModalValidator : AbstractValidator<UpdateBulkTableColumnWithModalDesignCommand>
{
    public UpdateBulkTableColumnWithModalValidator()
    {
        RuleFor(x => x.TableId)
            .NotEmpty().WithMessage("Güncellenecek table ID'si boş olamaz.");
    }
}