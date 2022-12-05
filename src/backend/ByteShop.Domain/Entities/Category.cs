namespace ByteShop.Domain.Entities;
public class Category : Entity
{
    public string Name { get; private set; }
    public int ParentId { get; private set; }
    public int SuperId { get; private set; }

    public List<Product> Products { get; set; }

    //For EF
    public Category(){}

    public Category(string name, int parentId)
    {
        Name = name;
        ParentId = parentId;
    }

    public Category(string name)
    {
        Name = name;
    }
}
