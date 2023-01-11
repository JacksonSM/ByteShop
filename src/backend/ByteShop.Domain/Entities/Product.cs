using ByteShop.Domain.DomainMessages;
using ByteShop.Domain.Entities.Validations;
using ByteShop.Domain.Interfaces.Mediator;

namespace ByteShop.Domain.Entities;
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
    public string MainImageUrl { get; private set; } = string.Empty;
    public string SecondaryImageUrl { get; private set; } = string.Empty;
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public bool IsActive { get; private set; }

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

    public void SetMainImage(string imageUrl)
    {
        MainImageUrl = imageUrl;
    }

    public void AddSecondaryImage(string imageUrl)
    {
        var exists = string.IsNullOrEmpty(MainImageUrl);
        if (exists)
            AddValidationError("MainImageUrl", ResourceDomainMessages.MUST_HAVE_A_MAIN_IMAGE);

        SecondaryImageUrl += imageUrl + " ";
    }

    public string[] GetSecondaryImageUrl()
    {
        if (SecondaryImageUrl is not null)
        {
            string[] urls = SecondaryImageUrl
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return urls;
        }
        else
        {
            return null;
        }
    }

    public int GetImagesTotal()
    {
        int total = 0;
        if (!string.IsNullOrEmpty(MainImageUrl)) total++;
        var secondary = GetSecondaryImageUrl();
        total += secondary?.Length ?? 0;
        return total;
    }

    public void Disable()
    {
        IsActive = false;
    }

    public void RemoveSecondaryImage(string url)
    {
        var urls = GetSecondaryImageUrl().ToList();
        urls.Remove(url);
        SetSecondaryImageUrl(urls.ToArray());
    }

    private void SetSecondaryImageUrl(string[] urls)
    {
        SecondaryImageUrl = string.Join(" ", urls);
    }

    public List<string> GetAllImages()
    {
        var images = new List<string>();
        images.Add(MainImageUrl);
        images.AddRange(GetSecondaryImageUrl());
        return images;
    }

    public override bool IsValid()
    {
        var validator = new ProductValidation();
        ValidationResult = validator.Validate(this);
        return ValidationResult.IsValid;
    }
}
