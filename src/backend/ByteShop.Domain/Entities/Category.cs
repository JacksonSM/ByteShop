using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;

namespace ByteShop.Domain.Entities;
public class Category : Entity
{
    public string Name { get; private set; }
    public int? ParentCategoryId { get; private set; }
    public Category ParentCategory { get; private set; }
    public List<Category> ChildCategories { get; private set; }

    public List<Product> Products { get; private set; }

    //For EF
    public Category(){}

    public Category(string name)
    {
        Name = name;
        ParentCategoryId = null;
    }

    public Category(string name, Category parentCategory)
    {
        ValidateLevel(parentCategory);
        Name = name;
        ParentCategoryId = parentCategory.Id;
    }

    public void AddChild(Category category)
    {
        ValidateLevel(category);
        if(ChildCategories != null)
            ChildCategories.Add(category);
    }

    private void ValidateLevel(Category category)
    {
        var exists = category.ParentCategory?.ParentCategory;
        DomainExecption.When(exists != null, ResourceDomainMessages.MAXIMUM_CATEGORY_LEVEL);
    }

    public void Update(string name, int parentCategoryId)
    {
        Name = name;
        if(parentCategoryId != 0)
            ParentCategoryId = parentCategoryId;
    }
}
