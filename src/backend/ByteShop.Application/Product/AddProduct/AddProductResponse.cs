using FluentValidation.Results;

namespace ByteShop.Application.Product.AddProduct;
public class AddProductResponse 
{
    public int? ProductId { get; set; }
    public ValidationResult ValidationResult { get; set; }

    public AddProductResponse(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }
    public AddProductResponse(int? productId, ValidationResult validationResult)
    {
        ProductId = productId == 0 ? null : productId;
        ValidationResult = validationResult;
    }
}
