using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Validations.Image;
using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Application.UseCases.Validations.Product;
public class AddProductValidation : AbstractValidator<AddProductCommand>
{
    private const int MAXIMUM_AMOUNT_OF_IMAGES = 5;

    public AddProductValidation()
    {
        RuleFor(x => x).SetValidator(new ProductValidation());
        RuleFor(x => x.MainImageBase64).SetValidator(new ImageBase64Validator());
        RuleForEach(x => x.SecondaryImagesBase64).SetValidator(new ImageBase64Validator());

        When(x => x.SecondaryImagesBase64?.Length > 0, () =>
        {
            RuleFor(x => x).Custom((command, context) =>
            {
                if (command.MainImageBase64 is null)
                    context.AddFailure(ResourceErrorMessages.MUST_HAVE_A_MAIN_IMAGE);

                int total = GetTotalAmountOfImages(command);
                if (total > MAXIMUM_AMOUNT_OF_IMAGES)
                    context.AddFailure(ResourceErrorMessages.MAXIMUM_AMOUNT_OF_IMAGES);

            });
        });
    }

    private static int GetTotalAmountOfImages(AddProductCommand command)
    {
        int total = command.SecondaryImagesBase64?.Length ?? 0;
        if (command.MainImageBase64 is not null) total++;
        return total;
    }
}
