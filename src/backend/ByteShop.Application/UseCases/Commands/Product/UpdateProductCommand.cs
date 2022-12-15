namespace ByteShop.Application.UseCases.Commands.Product;
public class UpdateProductCommand : ProductCommand
{
    public string MainImageUrl { get; set; }
    public string[] SecondaryImageUrl { get; set; } 
}
