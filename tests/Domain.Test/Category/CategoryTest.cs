using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
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

        var action = () =>
        {
            var newCategory = new ByteShop.Domain.Entities.Category("Memoria Ram", categoryParent);
        };

        action.Should().Throw<DomainExecption>()
            .Where(exception =>
            exception.Message.Contains(ResourceDomainMessages.MAXIMUM_CATEGORY_LEVEL));
    }

    [Fact]
    public void AtualizarComCategoriaPai()
    {
        var category = new ByteShop.Domain.Entities.Category("Fonte");

        category.Update("Fonte Real", 36);

        category.Name.Should().Be("Fonte Real");
        category.ParentCategoryId.Should().Be(36);
    }

    [Fact]
    public void AtualizarSemCategoriaPai()
    {
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        categoryParent.Id = 93;
        var category = new ByteShop.Domain.Entities.Category("Fonte", categoryParent);

        category.Update("Fonte Real", 0);

        category.Name.Should().Be("Fonte Real");
        category.ParentCategoryId.Should().Be(93);
    }
}
