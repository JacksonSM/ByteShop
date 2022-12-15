namespace ByteShop.Application.UseCases.Commands.Product;
public class AddProductCommand : ProductCommand
{
    public ImageBase64 MainImageBase64 { get; set; }
    public ImageBase64[] SecondaryImagesBase64 { get; set; }

}
public class ImageBase64
{
    public string Base64 { get; set; }
    public string Extension { get; set; }
}