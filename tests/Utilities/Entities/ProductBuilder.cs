using Bogus;
using ByteShop.Domain.Entities;

namespace Utilities.Entities;
public class ProductBuilder
{
    public static Product ProductBuild(int numberSecondaryImages = 3)
    {
        return new Faker<Product>()
            .RuleFor(c => c.Id, f => f.Random.Number(0, 500))
            .RuleFor(c => c.Name, f => f.Commerce.ProductName())
            .RuleFor(c => c.Description, f => f.Commerce.ProductDescription())
            .RuleFor(c => c.SKU, f => f.Commerce.Ean13())
            .RuleFor(c => c.Brand, f => f.Lorem.Word())
            .RuleFor(c => c.Price, f => f.Finance.Amount(min: 1))
            .RuleFor(c => c.CostPrice, f => f.Finance.Amount(min: 1))
            .RuleFor(c => c.Stock, f => f.Random.Number(1000))
            .RuleFor(c => c.Warranty, f => f.Random.Number(200))
            .RuleFor(c => c.Weight, f => f.Random.Float(1))
            .RuleFor(c => c.Height, f => f.Random.Float(1))
            .RuleFor(c => c.Width, f => f.Random.Float(1))
            .RuleFor(c => c.Length, f => f.Random.Float(1))
            .RuleFor(c => c.CategoryId, f => f.Random.Number(0, 500))
            .RuleFor(c => c.MainImageUrl, f => f.Internet.Url())
            .RuleFor(c => c.SecondaryImageUrl, f => string.Join(" ", f.Make(numberSecondaryImages,
            () => f.Internet.Url()).ToArray()));
    }
}
