using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Application.UseCases.Validations.Product;
public class ProductValidation : AbstractValidator<ProductCommand>
{
    public ProductValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.PRODUCT_NAME_EMPTY)
            .MaximumLength(60)
            .WithMessage(ResourceErrorMessages.PRODUCT_NAME_MAXIMUMLENGTH);

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.PRODUCT_DESCRIPTION_EMPTY);

        RuleFor(c => c.SKU)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.PRODUCT_SKU_EMPTY);

        RuleFor(c => c.Price)
            .PrecisionScale(18, 2, false);

        RuleFor(c => c.CostPrice)
            .PrecisionScale(18, 2, false);

        RuleFor(c => c.Weight)
            .LessThanOrEqualTo(0)
            .WithMessage(ResourceErrorMessages.PRODUCT_WEIGHT_LESS_OR_EQUAL_TO_ZERO);

        RuleFor(c => c.Height)
            .LessThanOrEqualTo(0)
            .WithMessage(ResourceErrorMessages.PRODUCT_HEIGHT_LESS_OR_EQUAL_TO_ZERO);

        RuleFor(c => c.Width)
            .LessThanOrEqualTo(0)
            .WithMessage(ResourceErrorMessages.PRODUCT_WIDTH__LESS_OR_EQUAL_TO_ZERO);

        RuleFor(c => c.Length)
            .LessThanOrEqualTo(0)
            .WithMessage(ResourceErrorMessages.PRODUCT_LENGTH_LESS_OR_EQUAL_TO_ZERO);



    }
}
