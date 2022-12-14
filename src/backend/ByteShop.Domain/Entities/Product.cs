namespace ByteShop.Domain.Entities;
public class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public decimal CostPrice { get; private set; }
    public int Stock { get; private set; }
    public int Warranty { get; private set; }
    public string Brand { get; private set; }
    public float Weight { get; private set; }
    public float Heigth { get; private set; }
    public float Lenght { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }

    public Product(){}

    public Product(
        string name, string description, decimal price,
        decimal costPrice, int stock, int warranty,
        string brand, float weight, float heigth, float lenght,
        int categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        CostPrice = costPrice;
        Stock = stock;
        Warranty = warranty;
        Brand = brand;
        Weight = weight;
        Heigth = heigth;
        Lenght = lenght;
        CategoryId = categoryId;
    }
}
