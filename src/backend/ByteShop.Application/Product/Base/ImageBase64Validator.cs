﻿using ByteShop.Application.Product.AddProduct;
using ByteShop.Domain.DomainMessages;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ByteShop.Application.Product.Base;
public class ImageBase64Validation : AbstractValidator<ImageBase64>
{
    private const int MAXIMUM_IMAGE_SIZE_IN_BYTES = 350000;

    public ImageBase64Validation()
    {
        When(c => !string.IsNullOrWhiteSpace(c.Base64) && !string.IsNullOrWhiteSpace(c.Extension), () =>
        {
            RuleFor(c => c.Base64).Custom((imageBase64, context) =>
            {
                var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(imageBase64, "");
                byte[] imageBytes = Array.Empty<byte>();
                try
                {
                    imageBytes = Convert.FromBase64String(data);
                }
                catch
                {
                    context.AddFailure(ResourceValidationErrorMessage.INVALID_BASE64);
                }
                if (imageBytes.Length > MAXIMUM_IMAGE_SIZE_IN_BYTES)
                    context.AddFailure(ResourceValidationErrorMessage.MAX_IMAGE_SIZE);
            });


            RuleFor(c => c.Extension).Custom((extension, context) =>
            {
                if (!ValidateExtension(extension))
                    context.AddFailure(ResourceValidationErrorMessage.IMAGE_EXTENSION);
            });
        }).Otherwise(() =>
        {
            RuleFor(c => c).Custom((image, context) =>
            {
                if (string.IsNullOrEmpty(image.Base64) || string.IsNullOrEmpty(image.Base64))
                    context.AddFailure(ResourceValidationErrorMessage.INCORRECT_IMAGE);
            });
        });
    }

    private bool ValidateExtension(string extension)
    {
        if (extension is null)
            return false;

        return ".jpeg".Equals(extension) ||
               ".jpg".Equals(extension) ||
               ".png".Equals(extension) ||
               ".webp".Equals(extension);
    }
}
