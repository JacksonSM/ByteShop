using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Entities;
using Xunit;

namespace Domain.Test.Category;
public class CategoryTest
{
    [Fact]
    public void TestarValidadorDeNivel()
    {
        var categoryParent = CategoryBuilder.BuildCategoryWithThreeLevels();

        var newCategory = new ByteShop.Domain.Entities.Category("Memoria Ram", categoryParent);
  

        newCategory.IsValid().Should().BeFalse();
        newCategory.ValidationResult.Errors.Any(error => error.ErrorMessage.Equals(ResourceDomainMessages.MAXIMUM_CATEGORY_LEVEL))
            .Should().BeTrue();
    }

    [Fact]
    public void AtualizarComCategoriaPai()
    {
        var oldFatherCategory = CategoryBuilder.BuildCategoryWithoutLevel();

        var category = CategoryBuilder.BuildCategoryWithoutLevel(oldFatherCategory);

        var newParentCategory = CategoryBuilder.BuildCategoryWithoutLevel();

        category.Update("Fonte Real", newParentCategory);

        category.Name.Should().Be("Fonte Real");
        category.ParentCategoryId.Should().Be(newParentCategory.Id);
    }

    [Fact]
    public void AtualizarSemCategoriaPai()
    {
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        categoryParent.Id = 93;
        var category = new ByteShop.Domain.Entities.Category("Fonte", categoryParent);

        category.Update("Fonte Real", null);

        category.Name.Should().Be("Fonte Real");
        category.ParentCategoryId.Should().Be(93);
    }
}
