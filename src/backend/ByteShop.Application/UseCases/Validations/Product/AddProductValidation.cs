using ByteShop.Application.UseCases.Commands.Product;
using FluentValidation;

namespace ByteShop.Application.UseCases.Validations.Product;
public class AddProductValidation : AbstractValidator<AddProductCommand>
{
	public AddProductValidation()
	{
        RuleFor(x => x).SetValidator(new ProductValidation());
    }
}

