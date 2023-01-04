using ByteShop.Application.Commands.Validations.Product;
using FluentValidation.Results;

namespace ByteShop.Application.Commands.Product;
public class AddProductCommand : ProductCommand
{
    public ImageBase64 MainImageBase64 { get; set; }
    public ImageBase64[] SecondaryImagesBase64 { get; set; }

    public ValidationResult Validate(bool IsThereCategory)
    {
        var validator = new AddProductCommandValidation(IsThereCategory);
        return validator.Validate(this);
    }
}
