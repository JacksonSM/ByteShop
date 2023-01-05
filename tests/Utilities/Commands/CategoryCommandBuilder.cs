using Bogus;
using ByteShop.Application.Category.AddCategory;
using ByteShop.Application.Category.UpdateCategory;

namespace Utilities.Commands;
public class CategoryCommandBuilder
{
    public static AddCategoryCommand AddCategoryCommandBuild()
    {
        return new Faker<AddCategoryCommand>()
            .RuleFor(c => c.Name, f => f.Commerce.Department())
            .RuleFor(c => c.ParentCategoryId, f => f.Random.Number(500));
    }
    public static UpdateCategoryCommand UpdateCategoryCommandBuild()
    {
        return new Faker<UpdateCategoryCommand>()
            .RuleFor(c => c.Name, f => f.Commerce.Department())
            .RuleFor(c => c.ParentCategoryId, f => f.Random.Number(500));
    }
}
