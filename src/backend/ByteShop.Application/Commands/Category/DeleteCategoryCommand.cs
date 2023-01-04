using ByteShop.Infra.CrossCutting.Bus;

namespace ByteShop.Application.Commands.Category;
public class DeleteCategoryCommand : Command
{
    public int Id { get; set; }

    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }
}
