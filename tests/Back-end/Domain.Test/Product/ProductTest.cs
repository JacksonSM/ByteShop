using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Xunit;

namespace Domain.Test.Product;
public class ProductTest
{
    [Fact]
    public void AdicionarImagemSegundariaSemDefinirAPrincipal()
    {
        var product = new ByteShop.Domain.Entities.Product
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

  
            product.AddSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");


        product.ValidationResult.Errors.Should().ContainSingle(ResourceDomainMessages.MUST_HAVE_A_MAIN_IMAGE);
    }

    [Fact]
    public void TestarGetImagesTotalSemImagens()
    {
        var product = new ByteShop.Domain.Entities.Product(
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
        var result = product.GetImagesTotal();

        result.Should().Be(0);
    }

    [Fact]
    public void TestarGetImagesTotalApenasComImagemPrincipal()
    {
        var product = new ByteShop.Domain.Entities.Product(
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
        product.SetMainImage("uma imagem qualquer");

        var result = product.GetImagesTotal();

        result.Should().Be(1);
    }

    [Fact]
    public void TestarGetImagesTotalComImagemPrincipalESegundaria()
    {
        var product = new ByteShop.Domain.Entities.Product
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
        product.SetMainImage("https://umaimagem.com");
        product.AddSecondaryImage("https://umaimagem.com");
        product.AddSecondaryImage("https://umaimagem.com");
        product.AddSecondaryImage("https://umaimagem.com");

        var result = product.GetImagesTotal();

        result.Should().Be(4);
    }

    [Fact]
    public void TestarAddSecondaryImage()
    {
        var product = new ByteShop.Domain.Entities.Product
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

        product.SetMainImage("https://m.media-amazon.com/images/I/712FFQs35IL._AC_SY741_.jpg");
        product.AddSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        var result = product.SecondaryImageUrl
            .Contains("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        result.Should().BeTrue();
    }

    [Fact]
    public void TestarRemoveSecondaryImage()
    {
        var product = new ByteShop.Domain.Entities.Product
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

        product.SetMainImage("https://m.media-amazon.com/images/I/712FFQs35IL._AC_SY741_.jpg");
        product.AddSecondaryImage("https://m.media-amazon.com/images/I/71VQ3m40TKL._AC_SL1500_.jpg");
        product.AddSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");
        product.AddSecondaryImage("https://m.media-amazon.com/images/I/71ZZ0N1fOEL._AC_SL1500_.jpg");

        product.RemoveSecondaryImage("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        var result = product.SecondaryImageUrl
            .Contains("https://m.media-amazon.com/images/I/71UU6EIWTBL._AC_SL1500_.jpg");

        result.Should().BeFalse();
    }
}
