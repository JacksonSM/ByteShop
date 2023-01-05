using ByteShop.Application.Product.Base;
using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Application.Product.AddProduct;

public class AddProductCommandValidation : AbstractValidator<AddProductCommand>
{
    private const int MAXIMUM_AMOUNT_OF_IMAGES = 5;

    public AddProductCommandValidation(bool IsThereCategory)
    {

        RuleFor(x => x).Custom((command, context) =>
        {
            if (!IsThereCategory)
                context.AddFailure("CategoryId", ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST);
        });

        RuleFor(c => c.MainImageBase64).SetValidator(new ImageBase64Validation());
        RuleForEach(c => c.SecondaryImagesBase64).SetValidator(new ImageBase64Validation());

        When(x => x.SecondaryImagesBase64?.Length > 0, () =>
        {
            RuleFor(x => x).Custom((command, context) =>
            {
                if (command.MainImageBase64 is null)
                    context.AddFailure(ResourceErrorMessages.MUST_HAVE_A_MAIN_IMAGE);

                int total = GetTotalAmountOfImages(command);
                if (total > MAXIMUM_AMOUNT_OF_IMAGES)
                {
                    context.AddFailure(ResourceErrorMessages.MAXIMUM_AMOUNT_OF_IMAGES);
                }


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