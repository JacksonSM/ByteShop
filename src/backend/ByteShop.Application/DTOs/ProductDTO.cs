namespace ByteShop.Application.DTOs;
public class ProductDTO
{
    public int Id { get; set; }
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
    public int CategoryId { get; set; }
    public DateTime CreatedOn { get; set; }
}
