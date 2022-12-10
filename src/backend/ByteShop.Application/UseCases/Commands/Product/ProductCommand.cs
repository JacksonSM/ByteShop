namespace ByteShop.Application.UseCases.Commands.Product;
public class ProductCommand : ICommand
{
    public int Id { get; protected set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public int Stock { get; set; }
    public int Warranty { get; set; }
    public string Brand { get; set; }
    public float Weight { get; set; }
    public float Heigth { get; set; }
    public float Lenght { get; set; }
    public string MainImageUrl { get; private set; }
    public string SecondaryImageUrl { get; private set; }
    public int CategoryId { get; set; }

    public void SetId(int id)
    {
        Id = id;
    }
}
