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
    public float Height { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public CategoryDTO Category { get; set; }
    public string MainImageUrl { get; set; }
    public string[] SecondaryImageUrl { get; set; }
    public bool IsActive { get;  set; }
    public DateTime CreatedOn { get; set; }
}
