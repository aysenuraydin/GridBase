
using FluentValidation;

namespace gridbase.Application.Features.MenuItems.Commands.UpdateMenuItem;

public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItemCommand>
{
    public UpdateMenuItemValidator()
    {
        RuleFor(x => x.Label).NotEmpty().WithMessage("Menü başlığı boş olamaz.");
    }
}