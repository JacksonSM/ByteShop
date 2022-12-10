namespace ByteShop.Application.UseCases.Commands.Category;
public class UpdateCategoryCommand : ICommand
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }

    public void SetId(int id)
    {
        Id = id;
    }
}
