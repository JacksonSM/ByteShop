namespace ByteShop.Application.UseCases.Commands.Product;
public class UpdateProductCommand : ProductCommand
{
    public string[] RemoveSecondaryImageUrl { get; set; }

    public ImageBase64 SetMainImageBase64 { get; set; }
    public ImageBase64[] AddSecondaryImageBase64 { get; set; }

    

    /// <summary>
    /// Retorna o valor total de imagens para serem removidas, 
    /// incluindo a imagem principal.
    /// </summary>
    public int GetTotalImagesToRemove()
    {
        int total = 0;
        if (SetMainImageBase64 is not null) total++;
        total += RemoveSecondaryImageUrl?.Length ?? 0;
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
}
