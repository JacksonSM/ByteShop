using ByteShop.Application.Product.AddProduct;
using ByteShop.Application.Product.Base;
using FluentValidation.Results;

namespace ByteShop.Application.Product.UpdateProduct;
public class UpdateProductCommand : ProductCommand<ValidationResult>
{
    public string[] RemoveImageUrl { get; set; }

    public ImageBase64 SetMainImageBase64 { get; set; }
    public string SetMainImageUrl { get; set; }
    public ImageBase64[] AddSecondaryImageBase64 { get; set; }

    /// <summary>
    /// Retorna o valor total de imagens para serem removidas, 
    /// incluindo a imagem principal.
    /// </summary>
    public int GetTotalImagesToRemove()
    {
        int total = 0;
        if (SetMainImageBase64 is not null) total++;
        total += RemoveImageUrl?.Length ?? 0;
        return total;
    }
    /// <summary>
    /// Retorna o valor total de imagens para serem adicionadas, 
    /// incluindo a imagem principal.
    /// </summary>
    public int GetTotalImagesToAdd()
    {
        int total = 0;
        if (SetMainImageBase64 != null) total++;
        total += AddSecondaryImageBase64?.Length ?? 0;
        return total;
    }
    public bool IsValid(Domain.Entities.Product product, bool IsThereCategory)
    {
        var validator = new UpdateProductCommandValidation(IsThereCategory, product);
        ValidationResult = validator.Validate(this);
        return ValidationResult.IsValid;
    }
}
