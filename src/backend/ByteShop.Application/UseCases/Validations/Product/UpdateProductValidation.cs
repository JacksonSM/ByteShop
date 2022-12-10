using ByteShop.Application.UseCases.Commands.Product;
using FluentValidation;

namespace ByteShop.Application.UseCases.Validations.Product;
public class UpdateProductValidation : AbstractValidator<UpdateProductCommand>
{
	public UpdateProductValidation()
	{
        RuleFor(x => x).SetValidator(new ProductValidation());
    }
}
