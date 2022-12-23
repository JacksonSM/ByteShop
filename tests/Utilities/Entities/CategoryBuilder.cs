using Bogus;
using ByteShop.Domain.Entities;

namespace Utilities.Entities;
public class CategoryBuilder
{

    public static Category BuildCategoryWithTwoLevels()
    {
        var categoryLevelOne = BuildCategoryWithoutLevel();
        var categoryLevelTwo = BuildCategoryWithoutLevel(categoryLevelOne);
        categoryLevelOne.AddChild(categoryLevelTwo);
        return categoryLevelOne;
    }

    public static Category BuildCategoryWithThreeLevels()
    {
        var categoryLevelOne = BuildCategoryWithoutLevel();
        var categoryLevelTwo = BuildCategoryWithoutLevel(categoryLevelOne);
        var categoryLevelThree = BuildCategoryWithoutLevel(categoryLevelTwo);
        categoryLevelOne.AddChild(categoryLevelTwo);
        categoryLevelTwo.AddChild(categoryLevelThree);
        return categoryLevelThree;
    }

    public static Category BuildCategoryWithoutLevel(Category category = null)
    {
        return new Faker<Category>()
            .RuleFor(c => c.Id, f => f.Random.Number(0, 500))
            .RuleFor(c => c.Name, f => f.Commerce.Department())
            .RuleFor(c => c.ParentCategory, category)
            .RuleFor(c => c.ParentCategoryId,  category?.Id ?? 0);
    }

}
