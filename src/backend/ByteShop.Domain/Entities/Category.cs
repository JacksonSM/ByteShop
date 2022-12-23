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
        ParentCategory = parentCategory;
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
        DomainExecption.When(exists != null, "Apenas 3 niveis de categoria são permitida.");
    }

    public void Update(string name, int parentCategoryId)
    {
        Name = name;
        if(parentCategoryId != 0)
            ParentCategoryId = parentCategoryId;
    }
}
