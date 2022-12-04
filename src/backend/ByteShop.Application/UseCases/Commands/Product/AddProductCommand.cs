namespace ByteShop.Application.UseCases.Commands.Product;
public class AddProductCommand : ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public int Stock { get; set; }
    public int Warranty { get; set; }
    public string Brand { get; set; }
    public float Weight { get; set; }
    public float Heigth { get; set; }
    public float Lenght { get; set; }
    public int CategoryId { get; set; }
}
