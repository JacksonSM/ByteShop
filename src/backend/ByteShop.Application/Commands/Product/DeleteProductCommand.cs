using ByteShop.Infra.CrossCutting.Bus;

namespace ByteShop.Application.Commands.Product;
public class DeleteProductCommand : Command
{
    public int Id { get; set; }
    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}
