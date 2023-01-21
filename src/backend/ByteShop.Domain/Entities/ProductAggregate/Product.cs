using ByteShop.Domain.Entities.Validations;
using ByteShop.Domain.Interfaces.Mediator;

namespace ByteShop.Domain.Entities.ProductAggregate;
public class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string SKU { get; private set; }
    public decimal Price { get; private set; }
    public decimal CostPrice { get; private set; }
    public int Stock { get; private set; }
    public int Warranty { get; private set; }
    public string Brand { get; private set; }
    public float Weight { get; private set; }
    public float Height { get; private set; }
    public float Length { get; private set; }
    public float Width { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public bool IsActive { get; private set; }
    public ImagesUrl ImagesUrl { get; } = new();


    public Product() { }

    public Product(
        string name, string description, string sku,
        decimal price, decimal costPrice, int stock,
        int warranty, string brand, float weight,
        float height, float length, float width, int categoryId)
    {
        Name = name;
        Description = description;
        SKU = sku;
        Price = price;
        CostPrice = costPrice;
        Stock = stock;
        Warranty = warranty;
        Brand = brand;
        Weight = weight;
        Height = height;
        Width = width;
        Length = length;
        CategoryId = categoryId;
        IsActive = true;
    }

    public void Update(
    string name, string description, string sku,
    decimal price, decimal costPrice, int stock,
    int warranty, string brand, float weight,
    float height, float length, float width, Category category)
    {
        Name = name;
        Description = description;
        SKU = sku;
        Price = price;
        CostPrice = costPrice;
        Stock = stock;
        Warranty = warranty;
        Brand = brand;
        Weight = weight;
        Height = height;
        Length = length;
        Width = width;

        if (category is not null)
        {
            CategoryId = category.Id;
            Category = category;
        }

    }

    public void Disable()
    {
        IsActive = false;
    }

    public override bool IsValid()
    {
        var validator = new ProductValidation();
        var result = validator.Validate(this);
        ValidationResult.Errors.AddRange(result.Errors);
        return ValidationResult.IsValid;
    }
}
