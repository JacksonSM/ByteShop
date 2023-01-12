using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Entities;
using Xunit;

namespace Domain.Test.Category;
public class CategoryTest
{
    [Fact]
    [Trait("Category", "MethodTest")]
    public void CategoryConstrutor_Created3LevelCategory_ValidCategory()
    {
        //Arrange
        var categoryParent = CategoryBuilder.BuildCategoryWithTwoLevels();

        //Act
        var newCategory = new ByteShop.Domain.Entities.Category("Placa de Video", categoryParent);

        //Assert
        newCategory.IsValid().Should().BeTrue();
        newCategory.Name.Should().BeSameAs("Placa de Video");
        newCategory.ParentCategoryId.Should().Be(categoryParent.Id);
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void CategoryConstrutor_Created4LevelCategory_InvalidCategory()
    {
        //Arrange
        var categoryParent = CategoryBuilder.BuildCategoryWithThreeLevels();

        //Act
        var newCategory = new ByteShop.Domain.Entities.Category("Memoria Ram", categoryParent);
        
        //Assert
        newCategory.IsValid().Should().BeFalse();
        newCategory.ValidationResult.Errors.Any(error => error.ErrorMessage.Equals(ResourceDomainMessages.MAXIMUM_CATEGORY_LEVEL))
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void Update_UpdateWithValidParentCategory_ValidCategory()
    {
        //Arrange
        var oldFatherCategory = CategoryBuilder.BuildCategoryWithoutLevel();
        var category = CategoryBuilder.BuildCategoryWithoutLevel(oldFatherCategory);
        var newParentCategory = CategoryBuilder.BuildCategoryWithoutLevel();

        //Act
        category.Update("Fonte Real", newParentCategory);

        //Assert
        category.Name.Should().Be("Fonte Real");
        category.ParentCategoryId.Should().Be(newParentCategory.Id);
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void Update_UpdateWithoutParentCategory_ValidCategory()
    {
        //Arrange
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        categoryParent.Id = 93;
        var category = new ByteShop.Domain.Entities.Category("Fonte", categoryParent);

        //Act
        category.Update("Fonte Real", null);

        //Assert
        category.Name.Should().Be("Fonte Real");
        category.ParentCategoryId.Should().Be(93);
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void CanItBeDeleted_WithoutCategoryChildAndAssociatedProducts_ShouldReturnTrue()
    {
        //Arrange
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        categoryParent.Id = 93;
        var category = new ByteShop.Domain.Entities.Category("Fonte", categoryParent);

        //Act
        var result = category.CanItBeDeleted();

        //Assert
        result.Should().BeTrue();   
    }

    [Fact]
    [Trait("Category", "MethodTest")]
    public void CanItBeDeleted_WithAssociatedProducts_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        categoryParent.Id = 93;
        var category = new ByteShop.Domain.Entities.Category("Fonte", categoryParent);
        var product = ProductBuilder.BuildProduct();
        category.AddProduct(product);

        //Act
        var result = category.CanItBeDeleted();

        //Assert
        result.Should().BeFalse();
    }
}
