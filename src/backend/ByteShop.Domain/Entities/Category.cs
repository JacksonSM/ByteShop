using ByteShop.Domain.Exceptions;

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

    public void AddChild(Category category)
    {
        var existe = ParentCategory?.ParentCategory;

        DomainExecption.When(existe != null, "Apenas 3 niveis de categoria são permitida.");

        if(ChildCategories != null)
            ChildCategories.Add(category);
    }
}
