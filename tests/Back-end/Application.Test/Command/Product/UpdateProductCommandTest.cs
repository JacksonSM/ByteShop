using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Entities;
using Xunit;

namespace Application.Test.Command.Product;
public class UpdateProductCommandTest
{
    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_CreateCommandWithValidData_ValidCommand()
    {
        //Arrange
        var product = ProductBuilder.BuildProduct();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);
        command.SetId(product.Id);

        //Act
        var result = command.IsValid(product,
            new ByteShop.Domain.Entities.Category());

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_DifferentIds_ValidCommand()
    {
        //Arrange
        var product = ProductBuilder.BuildProduct();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);
        command.SetId(product.Id + 4);

        //Act
        var result = command.IsValid(product,
            new ByteShop.Domain.Entities.Category());

        //Assert
        result.Should().BeFalse();
        command.ValidationResult.Errors.Should()
            .ContainSingle(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.ID_CONFLICT));
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_WithValuesInSetMainImageBase64AndSetMainImageUrl_InvalidCommand()
    {
        //Arrange
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);
        command.SetMainImageUrl = "https://www.google.com.br/";

        //Act
        var result = command.IsValid(new ByteShop.Domain.Entities.Product(),
            new ByteShop.Domain.Entities.Category());
            
        //Assert
        result.Should().BeFalse();
        command.ValidationResult.Errors
            .Should().ContainSingle(ResourceValidationErrorMessage.UPDATE_PRODUCT_WITH_INVALID_MAIN_IMAGE);
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_WithValuesInJustSetMainImageBase64_ValidCommand()
    {
        //Arrange
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);

        //Act
        var result = command.IsValid(new ByteShop.Domain.Entities.Product(),
            new ByteShop.Domain.Entities.Category());

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_WithValuesInJustSetMainImageUrl_ValidCommand()
    {
        //Arrange
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            changeMainImage: false,
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);
        command.SetMainImageUrl = "https://www.google.com.br/seila";

        //Act
        var result = command.IsValid(new ByteShop.Domain.Entities.Product(),
            new ByteShop.Domain.Entities.Category());

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_RemovesAnImageThatDoesNotExistInTheProduct_InvalidCommand()
    {
        //Arrange
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            changeMainImage: false,
            numberOfSecondaryImagesToRemove: 2,
            numberOfSecondaryImagesToAdd: 0);
        command.SetMainImageUrl = "https://www.google.com.br/seila";

        //Act
        var result = command.IsValid(new ByteShop.Domain.Entities.Product(),
            new ByteShop.Domain.Entities.Category());

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_ProductIsNull_InvalidCommand()
    {
        //Arrange
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            changeMainImage: false,
            numberOfSecondaryImagesToRemove: 2,
            numberOfSecondaryImagesToAdd: 0);
        command.SetMainImageUrl = "https://www.google.com.br/seila";

        //Act
        var result = command.IsValid(null,
            new ByteShop.Domain.Entities.Category());

        //Assert
        result.Should().BeFalse();
        command.ValidationResult.Errors
            .Should().ContainSingle(ResourceValidationErrorMessage.PRODUCT_DOES_NOT_EXIST);
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void IsValid_CategoryIsNull_InvalidCommand()
    {
        //Arrange
        var product = ProductBuilder.BuildProduct();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            changeMainImage: false,
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);
        command.SetMainImageUrl = "https://www.google.com.br/seila";
        command.SetId(product.Id);

        //Act
        var result = command.IsValid(product, null);

        //Assert
        result.Should().BeFalse();
        command.ValidationResult.Errors
            .Should().ContainSingle(ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST);
    }
}
