namespace ByteShop.Application.UseCases.Commands.Product;
public class UpdateProductCommand : ProductCommand
{
    public string RemoveMainImageUrl { get; set; }
    public string[] RemoveSecondaryImageUrl { get; set; }

    public ImageBase64 AddMainImageBase64 { get; set; }
    public ImageBase64[] AddSecondaryImageBase64 { get; set; }

    public bool AreThereImagesToAdd()
    {
       return AddMainImageBase64 != null ||
            AddSecondaryImageBase64?.Length > 0;
    }
    public int GetTotalImagesToRemove()
    {
        int total = 0;
        if (!string.IsNullOrEmpty(RemoveMainImageUrl)) total++;
        total += RemoveSecondaryImageUrl?.Length ?? 0;
        return total;
    }
    public int GetTotalImagesToAdd()
    {
        int total = 0;
        if (AddMainImageBase64 != null) total++;
        total += AddSecondaryImageBase64?.Length ?? 0;
        return total;
    }
}
