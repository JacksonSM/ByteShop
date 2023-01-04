using ByteShop.Infra.CrossCutting.Bus;

namespace ByteShop.Application.Commands.Category;
public class AddCategoryCommand : Command
{
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }
}
