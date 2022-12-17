using static System.Net.Mime.MediaTypeNames;

namespace ByteShop.Application.UseCases.Commands.Product;
public class AddProductCommand : ProductCommand
{
    public ImageBase64 MainImageBase64 { get; set; }
    public ImageBase64[] SecondaryImagesBase64 { get; set; }

    public bool AreThereImages()
    {
        return MainImageBase64 is not null ||
            SecondaryImagesBase64 is not null;
    }
    public bool MainImageHasItBeenDefined()
    {
        return MainImageBase64 is not null;
    }
}
public class ImageBase64
{
    public string Base64 { get; set; }
    public string Extension { get; set; }
}