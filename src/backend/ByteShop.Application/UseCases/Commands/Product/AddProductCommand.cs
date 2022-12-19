namespace ByteShop.Application.UseCases.Commands.Product;
public class AddProductCommand : ProductCommand
{
    public ImageBase64 MainImageBase64 { get; set; }
    public ImageBase64[] SecondaryImagesBase64 { get; set; }

    public bool MainImageHasItBeenDefined()
    {
        return MainImageBase64 is not null;
    }
    public int TotalImages()
    {
        int total = 0;
        if (MainImageBase64 is not null) total++;
        total += SecondaryImagesBase64?.Length ?? 0;
        return total;
    }
}
