using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Entities;
using Xunit;

namespace Domain.Test.Product;
public class AddProductValidationTest
{

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void ProductValidation_CreateProductWithValidData_ShouldReturnTrue()
    {
        //Arrange
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

        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyName_EmptyName_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
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

        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_NAME_EMPTY));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyName_NameWithMoreThan60Characters_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
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

        //Act 
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_NAME_MAXIMUMLENGTH));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyDescription_EmptyDescription_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
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
        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_DESCRIPTION_EMPTY));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertySKU_EmptySKU_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
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
        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_SKU_EMPTY));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyPrice_PriceWithNegativeValue_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "Mem-ram",
                price: -42.45m,
                costPrice: 440.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );
        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRICE_LESS_THAN_ZERO));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyCostPrice_CostPriceWithNegativeValue_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "Mem-ram",
                price: 342.45m,
                costPrice: -240.54m,
                stock: 45,
                warranty: 90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );
        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.COSTPRICE_LESS_THAN_ZERO));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyWarranty_WarrantyWithNegativeValue_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "Mem-ram",
                price: 342.45m,
                costPrice: 140.54m,
                stock: 45,
                warranty: -90,
                brand: "GOODRAM",
                weight: 43.56f,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );
        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.WARRANTY_LESS_THAN_ZERO));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyBrand_EmptyBrand_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var product = new ByteShop.Domain.Entities.Product
            (
                name: "Memoria ram",
                description: "Memoria da boa!",
                sku: "ME-RAM-4",
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
        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_BRAND_EMPTY));
    }

    [Fact]
    [Trait("Product", "PropertyValidation")]
    public void PropertyBrand_BrandWithMoreThan30Characters_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
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

        //Act
        var result = product.IsValid(); 

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_BRAND_MAXIMUMLENGTH));
    }

    [Trait("Product", "PropertyValidation")]
    [Theory]
    [InlineData(0)]
    [InlineData(-3.3f)]
    public void PropertyWeight_WeightWithValueLessThanOne_ShouldReturnFalseWithErrorMessage(float value)
    {
        //Arrange
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
                weight: value,
                height: 34.54f,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );
        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_WEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }

    [Trait("Product", "PropertyValidation")]
    [Theory]
    [InlineData(0)]
    [InlineData(-8.4f)]
    public void PropertyHeight_HeightWithValueLessThanOne_ShouldReturnFalseWithErrorMessage(float value)
    {
        //Arrange
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
                height: value,
                length: 65.82f,
                width: 76,
                categoryId: 34
            );

        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_HEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }

    [Trait("Product", "PropertyValidation")]
    [Theory]
    [InlineData(0)]
    [InlineData(-56.34f)]
    public void PropertyWidth_WidthWithValueLessThanOne_ShouldReturnFalseWithErrorMessage(float value)
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
                width: value,
                categoryId: 34
            );
        product.IsValid().Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_WIDTH_LESS_OR_EQUAL_TO_ZERO));
    }


    [Trait("Product", "PropertyValidation")]
    [Theory]
    [InlineData(0)]
    [InlineData(-43.3f)]
    public void PropertyLength_LengthWithValueLessThanOne_ShouldReturnFalseWithErrorMessage(float value)
    {
        //Arrange
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
                length: value,
                width: 76,
                categoryId: 34
            );

        //Act
        var result = product.IsValid();

        //Assert
        result.Should().BeFalse();
        product.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_LENGTH_LESS_OR_EQUAL_TO_ZERO));
    }

}
