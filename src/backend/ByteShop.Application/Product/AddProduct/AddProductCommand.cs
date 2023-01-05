using ByteShop.Application.Product.Base;
using FluentValidation.Results;

namespace ByteShop.Application.Product.AddProduct;
public class AddProductCommand : ProductCommand<AddProductResponse>
{
    public ImageBase64 MainImageBase64 { get; set; }
    public ImageBase64[] SecondaryImagesBase64 { get; set; }

    public ValidationResult Validate(bool IsThereCategory)
    {
        var validator = new AddProductCommandValidation(IsThereCategory);
        return validator.Validate(this);
    }
}
