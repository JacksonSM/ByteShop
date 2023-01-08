using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Entities;
using Xunit;

namespace Domain.Test.Category;
public class CategoryPropertyValidationTest
{
    [Fact]
    [Trait("Category", "FieldValidation")]
    public void CategoryValidation_CreateCategoryWithValidData_ShouldReturnTrue()
    {
        //Arrange
        var category = CategoryBuilder.BuildCategoryWithTwoLevels();

        //Act
        var result = category.IsValid();

        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    [Trait("Category", "FieldValidation")]
    public void PropertyName_EmptyName_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var category = new ByteShop.Domain.Entities.Category(string.Empty);

        //Act
        var result = category.IsValid();

        //Assert
        result.Should().BeFalse();
        category.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_NAME_EMPTY));
    }
    [Fact]
    [Trait("Category", "FieldValidation")]
    public void PropertyName_NameWithMoreThan50Characters_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var name = "ggggggggggggggggggggggggggggggggggggggggggggggggdddddddddddddddddfffffffffffffffffffffdddddddddddddddddddddddddddddddddddddgg";
        var category = new ByteShop.Domain.Entities.Category(name);

        //Act
        var result = category.IsValid();

        //Assert
        result.Should().BeFalse();
        category.ValidationResult.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_NAME_MAXIMUMLENGTH));
    }
}
