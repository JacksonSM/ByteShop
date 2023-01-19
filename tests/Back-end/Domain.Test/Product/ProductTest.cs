using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Entities;
using Xunit;

namespace Domain.Test.Product;
public class ProductTest
{
    [Fact]
    [Trait("Product", "MethodTest")]
    public void ProductConstrutor_WithValidData_ValidProduct()
    {
        //Act
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Teclado yamp",
                description: "Top",
                sku: "Teclado-yamp",
                price: 233.5m,
                costPrice: 115.6m,
                stock: 25,
                warranty: 30,
                brand: "Yamp",
                weight: 15f,
                height: 13f,
                length: 9f,
                width: 15f,
                categoryId: 36
            );

        //Assert
        product.Name.Should().BeSameAs("Teclado yamp");
        product.Description.Should().BeSameAs("Top");
        product.SKU.Should().BeSameAs("Teclado-yamp");
        product.Price.Should().Be(233.5m);
        product.CostPrice.Should().Be(115.6m);
        product.Stock.Should().Be(25);
        product.Warranty.Should().Be(30);
        product.Brand.Should().BeSameAs("Yamp");
        product.Weight.Should().Be(15f);
        product.Height.Should().Be(13f);
        product.Width.Should().Be(15f);
        product.CategoryId.Should().Be(36);
        product.IsActive.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void Update_WithNewCategory_ValidProduct()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Teclado Negativo",
                description: "naquele preço",
                sku: "Teclado-Negativo",
                price: 365.5m,
                costPrice: 256.6m,
                stock: 69,
                warranty: 130,
                brand: "Negativo",
                weight: 25f,
                height: 36f,
                length: 25f,
                width: 48f,
                categoryId: 36
            );
        var category = CategoryBuilder.BuildCategoryWithTwoLevels();

        //Act
        product.Update
            (
                name: "Teclado yamp",
                description: "Top",
                sku: "Teclado-yamp",
                price: 233.5m,
                costPrice: 115.6m,
                stock: 25,
                warranty: 30,
                brand: "Yamp",
                weight: 15f,
                height: 13f,
                length: 9f,
                width: 15f,
                category: category
            );

        //Assert
        product.Name.Should().BeSameAs("Teclado yamp");
        product.Description.Should().BeSameAs("Top");
        product.SKU.Should().BeSameAs("Teclado-yamp");
        product.Price.Should().Be(233.5m);
        product.CostPrice.Should().Be(115.6m);
        product.Stock.Should().Be(25);
        product.Warranty.Should().Be(30);
        product.Brand.Should().BeSameAs("Yamp");
        product.Weight.Should().Be(15f);
        product.Height.Should().Be(13f);
        product.Width.Should().Be(15f);
        product.CategoryId.Should().Be(category.Id);
        product.IsActive.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void Update_WithoutNewCategory_ValidProduct()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Teclado Negativo",
                description: "naquele preço",
                sku: "Teclado-Negativo",
                price: 365.5m,
                costPrice: 256.6m,
                stock: 69,
                warranty: 130,
                brand: "Negativo",
                weight: 25f,
                height: 36f,
                length: 25f,
                width: 48f,
                categoryId: 36
            );

        //Act
        product.Update
            (
                name: "Teclado yamp",
                description: "Top",
                sku: "Teclado-yamp",
                price: 233.5m,
                costPrice: 115.6m,
                stock: 25,
                warranty: 30,
                brand: "Yamp",
                weight: 15f,
                height: 13f,
                length: 9f,
                width: 15f,
                category: null
            );

        //Assert
        product.Name.Should().BeSameAs("Teclado yamp");
        product.Description.Should().BeSameAs("Top");
        product.SKU.Should().BeSameAs("Teclado-yamp");
        product.Price.Should().Be(233.5m);
        product.CostPrice.Should().Be(115.6m);
        product.Stock.Should().Be(25);
        product.Warranty.Should().Be(30);
        product.Brand.Should().BeSameAs("Yamp");
        product.Weight.Should().Be(15f);
        product.Height.Should().Be(13f);
        product.Width.Should().Be(15f);
        product.CategoryId.Should().Be(36);
        product.IsActive.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void AddSecondaryImage_AddSecondaryImageWithoutValueToMainImage_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Mouse razer",
                description: "Bom de mais",
                sku: "mouse-razer",
                price: 23.5m,
                costPrice: 15.6m,
                stock: 98,
                warranty: 90,
                brand: "Razer",
                weight: 23f,
                height: 10f,
                length: 9f,
                width: 34f,
                categoryId: 65
            );

        //Act
        product.ImagesUrl.AddSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle(ResourceDomainMessages.MUST_HAVE_A_MAIN_IMAGE);
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void GetImagesTotal_NoImage_ShouldReturn0()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product(
                name: "Mouse razer",
                description: "Bom de mais",
                sku: "mouse-razer",
                price: 23.5m,
                costPrice: 15.6m,
                stock: 98,
                warranty: 90,
                brand: "Razer",
                weight: 23f,
                height: 10f,
                length: 9f,
                width: 34f,
                categoryId: 65
            );

        //Act
        var result = product.ImagesUrl.GetImagesTotal();

        //Assert
        result.Should().Be(0);
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void GetImagesTotal_MainImageOnly_ShouldReturn1()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product(
                name: "Mouse razer",
                description: "Bom de mais",
                sku: "mouse-razer",
                price: 23.5m,
                costPrice: 15.6m,
                stock: 98,
                warranty: 90,
                brand: "Razer",
                weight: 23f,
                height: 10f,
                length: 9f,
                width: 34f,
                categoryId: 65
            );
        product.ImagesUrl.SetMainImage("https://thumbs.dreamstime.com/z/helo-informal-hindi-word-hello-handwritten-white-background-171060017.jpg");

        //Act
        var result = product.ImagesUrl.GetImagesTotal();

        //Assert
        result.Should().Be(1);
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void GetImagesTotal_WithMainImageAnd3SecondaryOnes_ShouldReturn4()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Mouse razer",
                description: "Bom de mais",
                sku: "mouse-razer",
                price: 23.5m,
                costPrice: 15.6m,
                stock: 98,
                warranty: 90,
                brand: "Razer",
                weight: 23f,
                height: 10f,
                length: 9f,
                width: 34f,
                categoryId: 65
            );

        product.ImagesUrl.SetMainImage("https://umaimagem.com");
        product.ImagesUrl.AddSecondaryImage("https://umaimagem.com");
        product.ImagesUrl.AddSecondaryImage("https://umaimagem.com");
        product.ImagesUrl.AddSecondaryImage("https://umaimagem.com");

        //Act
        var result = product.ImagesUrl.GetImagesTotal();

        //Assert
        result.Should().Be(4);
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void AddSecondaryImage_WithValidData_TheSecondaryImageUrlPropertyMustContainTheNewImage()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Mouse razer",
                description: "Bom de mais",
                sku: "mouse-razer",
                price: 23.5m,
                costPrice: 15.6m,
                stock: 98,
                warranty: 90,
                brand: "Razer",
                weight: 23f,
                height: 10f,
                length: 9f,
                width: 34f,
                categoryId: 65
            );

        product.ImagesUrl.SetMainImage("https://m.media-amazon.com/images/I/712FFQs35IL._AC_SY741_.jpg");
        product.ImagesUrl.AddSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        //Act
        var result = product.ImagesUrl.SecondaryImages
            .Contains("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void AddSecondaryImage_ProductNoMainImage_InvalidProduct()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Mouse razer",
                description: "Bom de mais",
                sku: "mouse-razer",
                price: 23.5m,
                costPrice: 15.6m,
                stock: 98,
                warranty: 90,
                brand: "Razer",
                weight: 23f,
                height: 10f,
                length: 9f,
                width: 34f,
                categoryId: 65
            );

        //Act
        product.ImagesUrl.AddSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        //Assert
        var result = product.IsValid();
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceDomainMessages.MUST_HAVE_A_MAIN_IMAGE));
    }

    [Fact]
    [Trait("Product", "MethodTest")]
    public void RemoveSecondaryImage_WithValidData_ImageMustBeRemovedFromSecondaryImages()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.ProductAggregate.Product
            (
                name: "Mouse razer",
                description: "Bom de mais",
                sku: "mouse-razer",
                price: 23.5m,
                costPrice: 15.6m,
                stock: 98,
                warranty: 90,
                brand: "Razer",
                weight: 23f,
                height: 10f,
                length: 9f,
                width: 34f,
                categoryId: 65
            );
        product.ImagesUrl.SetMainImage("https://m.media-amazon.com/images/I/712FFQs35IL._AC_SY741_.jpg");
        product.ImagesUrl.AddSecondaryImage("https://m.media-amazon.com/images/I/71VQ3m40TKL._AC_SL1500_.jpg");
        product.ImagesUrl.AddSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");
        product.ImagesUrl.AddSecondaryImage("https://m.media-amazon.com/images/I/71ZZ0N1fOEL._AC_SL1500_.jpg");

        //Act
        product.ImagesUrl.RemoveSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        //Assert
        product.ImagesUrl.SecondaryImages
            .Should()
            .NotContain("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");
    }
}
