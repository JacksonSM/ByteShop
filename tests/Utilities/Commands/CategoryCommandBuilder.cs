using Bogus;
using ByteShop.Application.Commands.Category;

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
