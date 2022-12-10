using ByteShop.Application.UseCases.Commands.Category;
using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Application.UseCases.Validations.Category;
public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.CATEGORY_NAME_EMPTY)
            .MaximumLength(50)
            .WithMessage(ResourceErrorMessages.CATEGORY_NAME_MAXIMUMLENGTH);
    }
}
