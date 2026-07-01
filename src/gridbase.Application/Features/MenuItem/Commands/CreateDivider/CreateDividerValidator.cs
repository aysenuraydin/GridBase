
using FluentValidation;

namespace gridbase.Application.Features.MenuItems.Commands.CreateDivider;

public class CreateDividerValidator : AbstractValidator<CreateDividerCommand>
{
    public CreateDividerValidator()
    {
        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("Divider başlığı boş olamaz.")
            .MaximumLength(50).WithMessage("Başlık çok uzun.");
    }
}