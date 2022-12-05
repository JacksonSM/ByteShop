namespace ByteShop.Application.UseCases.Commands.Category;
public class AddCategoryCommand : ICommand
{
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }
}
