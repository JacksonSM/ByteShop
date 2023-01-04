using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Domain.Entities.Validations;
public class CategoryValidation : AbstractValidator<Category>
{
    public CategoryValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.CATEGORY_NAME_EMPTY)
            .MaximumLength(50)
            .WithMessage(ResourceErrorMessages.CATEGORY_NAME_MAXIMUMLENGTH);

        RuleFor(c => c.CategoryLevel)
            .IsInEnum()
            .WithMessage(ResourceDomainMessages.MAXIMUM_CATEGORY_LEVEL);
    }
}
