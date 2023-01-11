﻿using ByteShop.Application.Product.Base;
using ByteShop.Domain.DomainMessages;
using FluentValidation;
using FluentValidation.Results;

namespace ByteShop.Application.Product.UpdateProduct;
public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    private const int MAXIMUM_AMOUNT_OF_IMAGES = 5;

    public UpdateProductCommandValidation(Domain.Entities.Category category, Domain.Entities.Product product)
    {
        RuleFor(x => x.SetMainImageBase64)
            .Must(x => x is null)
            .When(x => !string.IsNullOrEmpty(x.SetMainImageUrl))
            .WithMessage(ResourceValidationErrorMessage.UPDATE_PRODUCT_WITH_INVALID_MAIN_IMAGE);

        RuleFor(x => x.SetMainImageUrl)
            .Must(x => x is null)
            .When(x => x.SetMainImageBase64 is not null)
            .WithMessage(ResourceValidationErrorMessage.UPDATE_PRODUCT_WITH_INVALID_MAIN_IMAGE);


        RuleFor(x => x).Custom((command, context) =>
        {
            ThereIsImage(command.RemoveImageUrl, product.GetAllImages(), context);
        });

        RuleFor(x => x).Custom((command, context) =>
        {

            if (product == null)
                context.AddFailure("Id", ResourceValidationErrorMessage.PRODUCT_DOES_NOT_EXIST);

            if (category is null)
                context.AddFailure("CategoryId", ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST);
        });

        RuleFor(x => x.SetMainImageBase64).SetValidator(new ImageBase64Validation());
        RuleForEach(x => x.AddSecondaryImageBase64).SetValidator(new ImageBase64Validation());

        When(x => x.AddSecondaryImageBase64?.Length > 0, () =>
        {
            RuleFor(x => x).Custom((command, context) =>
            {
                if (string.IsNullOrEmpty(product?.MainImageUrl)
                    && command.SetMainImageBase64 is null)
                    context.AddFailure(ResourceValidationErrorMessage.MUST_HAVE_A_MAIN_IMAGE);

                int total = GetTotalAmountOfImages(product, command);
                if (total > MAXIMUM_AMOUNT_OF_IMAGES)
                    context.AddFailure(ResourceValidationErrorMessage.MAXIMUM_AMOUNT_OF_IMAGES);

            });
        });
    }

    private void ThereIsImage(
        string[] removeImageUrl,
        List<string> productImages,
        ValidationContext<UpdateProductCommand> context)
    {
        foreach (var imageUrl in removeImageUrl)
        {
            if(!productImages.Exists(x => x.Equals(imageUrl)))
            {
                var error = new ValidationFailure
                    (
                        "RemoveImageUrl",
                        ResourceValidationErrorMessage.IMAGE_URL_DOES_NOT_EXIST,
                        imageUrl
                    );
                context.AddFailure(error);
            }
        }
    }

    private static int GetTotalAmountOfImages(
        Domain.Entities.Product product, UpdateProductCommand command)
    {
        if (product == null) return 0;
        var afterRemoved = product.GetImagesTotal() - command.GetTotalImagesToRemove();
        var final = afterRemoved + command.GetTotalImagesToAdd();
        return final;
    }
}