namespace ByteShop.Domain.Entities;
public class Category : Entity
{
    public string Name { get; set; }
    public int ParentCategoryId { get; private set; }

    //For EF
    public Category(){}

    public Category(string name, int parentCategoryId)
    {
        Name = name;
        ParentCategoryId = parentCategoryId;
    }

    public Category(string name)
    {
        Name = name;
    }
}
