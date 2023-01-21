using Bogus;
using ByteShop.Application.Product.AddProduct;
using ByteShop.Application.Product.UpdateProduct;

namespace Utilities.Commands;
public class ProductCommandBuilder
{
    public static AddProductCommand AddProductCommandBuild(
        int numberSecondaryImages = 3,
        bool withImageOfMoreThan350KB = false)
    {
        var imagesFaker = new Faker<ImageBase64>()
            .RuleFor(c => c.Base64, f => f.PickRandom(GetBase64List()))
            .RuleFor(c => c.Extension, f => f.PickRandom<string>
            (new string[]{ ".webp", ".png", ".jpg", ".jpeg"}));


        var commandFaker = new Faker<AddProductCommand>()
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
            .RuleFor(c => c.CategoryId, f => f.Random.Number(0,500))
            .RuleFor(c => c.SecondaryImagesBase64, imagesFaker.Generate(numberSecondaryImages).ToArray());

        if (withImageOfMoreThan350KB)
        {
            var image = imagesFaker.Generate();
            image.Base64 = GetImageLargerThan350KB();
            commandFaker.RuleFor(c => c.MainImageBase64, image);
        }
        else
        {
            commandFaker.RuleFor(c => c.MainImageBase64, imagesFaker.Generate());
        }
        return commandFaker;
    }

    public static UpdateProductCommand UpdateProductCommandBuild(
        bool changeMainImage = true,
        int numberOfSecondaryImagesToAdd = 2,
        int numberOfSecondaryImagesToRemove = 2,
        bool withImageOfMoreThan350KB = false)
    {
        var imagesFaker = new Faker<ImageBase64>()
            .RuleFor(c => c.Base64, f => f.PickRandom(GetBase64List()))
            .RuleFor(c => c.Extension, f => f.PickRandom
            (new string[] { ".webp", ".png", ".jpg", ".jpeg" }));

        var commandFaker = new Faker<UpdateProductCommand>()
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
            .RuleFor(c => c.AddSecondaryImageBase64, imagesFaker.Generate(numberOfSecondaryImagesToAdd).ToArray())
            .RuleFor(c => c.RemoveImageUrl, f => f.Make(numberOfSecondaryImagesToRemove,
            () => f.Internet.Url()).ToArray());

        if (changeMainImage)
        {
            commandFaker.RuleFor(c => c.SetMainImageBase64, imagesFaker.Generate());
        }

        if (withImageOfMoreThan350KB)
        {
            var image = imagesFaker.Generate();
            image.Base64 = GetImageLargerThan350KB();
            var instance = commandFaker.Generate();
            instance.SetMainImageBase64 = image;
            return instance;
        }
        else
        {
            return commandFaker.Generate();
        } 
    }

    public static string GetPath(string fileName)
    {
        try
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return Path.Combine(startupPath, $"Utilities\\Commands\\Base64\\{fileName}");
        }
        catch
        {
            return string.Empty;
        }
    }
    public static string[] GetBase64List()
    {
        var path = GetPath("valid-images.txt");
        try
        {
            var content = File.ReadAllText(path);
            var images = content.Split(" ");
            return images;
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public static string GetImageLargerThan350KB()
    {
        var path = GetPath("images-larger-than-350KB.txt");
        try
        {
            var image = File.ReadAllText(path);
            return image;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}

