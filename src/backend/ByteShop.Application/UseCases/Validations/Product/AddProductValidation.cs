using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Application.UseCases.Validations.Product;
public class AddProductValidation : AbstractValidator<AddProductCommand>
{
	public AddProductValidation()
	{
		RuleFor(c => c.Name)
			.NotEmpty()
			.WithMessage(ResourceErrorMessages.PRODUCT_NAME_EMPTY)
			.MaximumLength(60)
			.WithMessage(ResourceErrorMessages.PRODUCT_NAME_MAXIMUMLENGTH);

		RuleFor(c => c.Description)
			.NotEmpty()
			.WithMessage(ResourceErrorMessages.PRODUCT_DESCRIPTION_EMPTY);

		RuleFor(c => c.Price)
			.PrecisionScale(18, 2, false);
		
		RuleFor(c => c.CostPrice)
			.PrecisionScale(18, 2, false);
	}
}
