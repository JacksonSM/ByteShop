using ByteShop.Application.Product.Base;
using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Application.Product.UpdateProduct;
public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    private const int MAXIMUM_AMOUNT_OF_IMAGES = 5;

    public UpdateProductCommandValidation(bool IsThereCategory, Domain.Entities.Product product)
    {
        RuleFor(x => x).Custom((command, context) =>
        {
            if (product == null)
                context.AddFailure("Id", ResourceErrorMessages.PRODUCT_DOES_NOT_EXIST);

            if (!IsThereCategory)
                context.AddFailure("CategoryId", ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST);
        });

        RuleFor(x => x.SetMainImageBase64).SetValidator(new ImageBase64Validation());
        RuleForEach(x => x.AddSecondaryImageBase64).SetValidator(new ImageBase64Validation());

        When(x => x.AddSecondaryImageBase64?.Length > 0, () =>
        {
            RuleFor(x => x).Custom((command, context) =>
            {
                if (string.IsNullOrEmpty(product?.MainImageUrl)
                    && command.SetMainImageBase64 is null)
                    context.AddFailure(ResourceErrorMessages.MUST_HAVE_A_MAIN_IMAGE);

                int total = GetTotalAmountOfImages(product, command);
                if (total > MAXIMUM_AMOUNT_OF_IMAGES)
                    context.AddFailure(ResourceErrorMessages.MAXIMUM_AMOUNT_OF_IMAGES);

            });
        });
    }

    private static int GetTotalAmountOfImages(Domain.Entities.Product product, UpdateProductCommand command)
    {
        if (product == null) return 0;
        var afterRemoved = product.GetImagesTotal() - command.GetTotalImagesToRemove();
        var final = afterRemoved + command.GetTotalImagesToAdd();
        return final;
    }
}