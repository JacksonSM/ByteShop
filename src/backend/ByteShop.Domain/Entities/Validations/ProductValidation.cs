using ByteShop.Domain.DomainMessages;
using FluentValidation;

namespace ByteShop.Domain.Entities.Validations;
public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_NAME_EMPTY)
            .MaximumLength(60)
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_NAME_MAXIMUMLENGTH);

        RuleFor(c => c.Brand)
            .NotEmpty()
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_BRAND_EMPTY)
            .MaximumLength(30)
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_BRAND_MAXIMUMLENGTH);

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_DESCRIPTION_EMPTY);

        RuleFor(c => c.SKU)
            .NotEmpty()
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_SKU_EMPTY);

        RuleFor(c => c.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ResourceValidationErrorMessage.PRICE_LESS_THAN_ZERO);

        RuleFor(c => c.CostPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ResourceValidationErrorMessage.COSTPRICE_LESS_THAN_ZERO);

        RuleFor(c => c.Warranty)
            .GreaterThanOrEqualTo(0)
            .WithMessage(ResourceValidationErrorMessage.WARRANTY_LESS_THAN_ZERO);

        RuleFor(c => c.Weight)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_WEIGHT_LESS_OR_EQUAL_TO_ZERO);

        RuleFor(c => c.Height)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_HEIGHT_LESS_OR_EQUAL_TO_ZERO);

        RuleFor(c => c.Width)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_WIDTH_LESS_OR_EQUAL_TO_ZERO);

        RuleFor(c => c.Length)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_LENGTH_LESS_OR_EQUAL_TO_ZERO);

        RuleFor(c => c.CategoryId)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ResourceValidationErrorMessage.PRODUCT_CATEGORYID_LESS_OR_EQUAL_TO_ZERO);

    }
}
