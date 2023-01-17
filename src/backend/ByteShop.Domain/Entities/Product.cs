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
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    public bool IsActive { get; private set; }

    public string MainImageUrl { get; private set; } = string.Empty;
    private string secondaryImageUrl = string.Empty;

    public List<string> SecondaryImageUrl 
    { 
        get => secondaryImageUrl.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
    }



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
        if (string.IsNullOrEmpty(MainImageUrl))
            AddValidationError("MainImageUrl", ResourceDomainMessages.MUST_HAVE_A_MAIN_IMAGE);

        if (SecondaryImageUrl.Count == 0)
            secondaryImageUrl = imageUrl;
        else
            secondaryImageUrl += $" {imageUrl}";
    }
    public void AddSecondaryImage(string[] imageUrls)
    {
        if (string.IsNullOrEmpty(MainImageUrl))
            AddValidationError("MainImageUrl", ResourceDomainMessages.MUST_HAVE_A_MAIN_IMAGE);

        foreach (var url in imageUrls)
        {
            AddSecondaryImage(url);
        }
    }

    public int GetImagesTotal()
    {
        int total = 0;
        if (!string.IsNullOrEmpty(MainImageUrl)) total++;
        total += SecondaryImageUrl?.Count ?? 0;
        return total;
    }

    public void Disable()
    {
        IsActive = false;
    }

    public void RemoveSecondaryImage(string url)
    {
        var result = SecondaryImageUrl.Remove(url);
        if (result)
        {
            secondaryImageUrl = string.Join(" ", SecondaryImageUrl);
        }
        else
        {
            AddValidationError("SecondaryImageUrl",
                ResourceValidationErrorMessage.IMAGE_URL_DOES_NOT_EXIST);
        }
    }


    public List<string> GetAllImages()
    {
        var images = new List<string>();
        images.Add(MainImageUrl);
        images.AddRange(SecondaryImageUrl);
        return images;
    }

    public override bool IsValid()
    {
        var validator = new ProductValidation();
        var result = validator.Validate(this);
        ValidationResult.Errors.AddRange(result.Errors);
        return ValidationResult.IsValid;
    }
}
