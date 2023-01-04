using ByteShop.Domain.Entities.Validations;
using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Xunit;

namespace Validators.Test.Product;
public class AddProductValidationTest
{
    [Fact]
    public void Sucesso()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeTrue();
    }

    [Fact]
    public void ValidarErroNomeVazio()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_NAME_EMPTY));
    }
    [Fact]
    public void ValidarErroNomeMaiorQue60Caracteres()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria rappppppppppppppppppppiiiiiiiiiiiiiiiiiipppuuuuuuuuuuuuuuuuuuuuuuuuum",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_NAME_MAXIMUMLENGTH));
    }

    [Fact]
    public void ValidarErroMarcaVazio()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_BRAND_EMPTY));
    }
    [Fact]
    public void ValidarErroMarcaMaiorQue30Caracteres()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOggggggggggggggggggggggggggggggggggggggggggggggggggggODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_BRAND_MAXIMUMLENGTH));
    }

    [Fact]
    public void ValidarErroDescricaoVazio()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_DESCRIPTION_EMPTY));
    }
    [Fact]
    public void ValidarErroSKUVazio()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_SKU_EMPTY));
    }
    [Fact]
    public void ValidarErroPesoMenorOuIgualZero()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 0,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_WEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroAlturaMenorOuIgualZero()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 0,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );
        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_HEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroLarguraMenorOuIgualZero()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 0,
                categoryId: 34
            );
        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_WIDTH_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroComprimentoMenorOuIgualZero()
    {
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "RAM-234",
                price: 342.45m,
                costPrice: 240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 0,
                width: 76,
                categoryId: 34
            );

        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_LENGTH_LESS_OR_EQUAL_TO_ZERO));
    }
  
}
