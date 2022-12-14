namespace ByteShop.Application.UseCases.Commands.Product;
public class AddProductCommand : ProductCommand
{
    public Image MainImageBase64 { get; set; }
    public Image[] SecondaryImagesBase64 { get; set; }

}
public class Image
{
    public string imageBase64 { get; set; }
    public string extension { get; set; }
}