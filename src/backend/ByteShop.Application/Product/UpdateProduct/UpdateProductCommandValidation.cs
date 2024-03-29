﻿using ByteShop.Application.Product.Base;
using ByteShop.Domain.DomainMessages;
using FluentValidation;

namespace ByteShop.Application.Product.UpdateProduct;
public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    private const int MAXIMUM_AMOUNT_OF_IMAGES = 5;

    public UpdateProductCommandValidation(Domain.Entities.Category category, Domain.Entities.ProductAggregate.Product product)
    {
        if (product is null)
        {
            RuleFor(x => x.Id)
                .Must(x => product is not null)
                .WithMessage(ResourceValidationErrorMessage.PRODUCT_DOES_NOT_EXIST);
        }
        else
        {
            RuleFor(x => x.Id)
                .Equal(product.Id)
                .WithMessage(ResourceValidationErrorMessage.ID_CONFLICT);

            IsThereAnImageToRemove(product);
            ValidateMainImage(product);
        }

        RuleFor(x => x.CategoryId)
            .Must(x => category is not null)
            .WithMessage(ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST);

        RuleFor(x => x.SetMainImageBase64)
            .Must(x => x is null)
            .When(x => !string.IsNullOrEmpty(x.SetMainImageUrl))
            .WithMessage(ResourceValidationErrorMessage.UPDATE_PRODUCT_WITH_INVALID_MAIN_IMAGE);

        RuleFor(x => x.SetMainImageBase64).SetValidator(new ImageBase64Validation());
        RuleForEach(x => x.AddSecondaryImageBase64).SetValidator(new ImageBase64Validation());
    }

    private void ValidateMainImage(Domain.Entities.ProductAggregate.Product product)
    {
        When(x => x.AddSecondaryImageBase64?.Length > 0, () =>
        {
            RuleFor(x => x).Custom((command, context) =>
            {
                if (string.IsNullOrEmpty(product?.ImagesUrl .MainImageUrl)
                    && command.SetMainImageBase64 is null)
                    context.AddFailure(ResourceValidationErrorMessage.MUST_HAVE_A_MAIN_IMAGE);

                int total = GetTotalAmountOfImages(product, command);
                if (total > MAXIMUM_AMOUNT_OF_IMAGES)
                    context.AddFailure(ResourceValidationErrorMessage.MAXIMUM_AMOUNT_OF_IMAGES);

            });
        });
    }

    private void IsThereAnImageToRemove(Domain.Entities.ProductAggregate.Product product)
    {
        When(x => x.RemoveImageUrl?.Length > 0, () =>
        {
            RuleForEach(x => x.RemoveImageUrl)
                .Must(url => product.ImagesUrl.GetAll().Contains(url))
                .WithMessage(url => ResourceValidationErrorMessage.IMAGE_URL_DOES_NOT_EXIST);
        });
    }

    private static int GetTotalAmountOfImages(
        Domain.Entities.ProductAggregate.Product product, UpdateProductCommand command)
    {
        if (product == null) return 0;

        return product.ImagesUrl.GetImagesTotal()
            - command.GetTotalImagesToRemove()
            + command.GetTotalImagesToAdd();
    }


}