using Bogus;
using ByteShop.Application.UseCases.Commands.Product;

namespace Utilities.Commands;
public class ProductCommandBuilder
{
    public static AddProductCommand AddProductCommandBuild(int numberSecondaryImages = 3)
    {
        var imagesFaker = new Faker<ImageBase64>()
            .RuleFor(c => c.Base64, f => f.Lorem.Text())
            .RuleFor(c => c.Extension, f => f.PickRandom<string>
            (new string[]{ ".webp", ".jpe", ".jpg", ".jpeg"}));


        return new Faker<AddProductCommand>()
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
            .RuleFor(c => c.CategoryId, f => f.Random.Number(500))
            .RuleFor(c => c.MainImageBase64, imagesFaker.Generate())
            .RuleFor(c => c.SecondaryImagesBase64, imagesFaker.Generate(numberSecondaryImages).ToArray());
    }
}