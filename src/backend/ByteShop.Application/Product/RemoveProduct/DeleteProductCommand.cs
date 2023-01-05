using ByteShop.Application.Configuration.Command;
using FluentValidation.Results;

namespace ByteShop.Application.Product.RemoveProduct;
public class DeleteProductCommand : CommandBase<ValidationResult>
{
    public int Id { get; set; }
    public DeleteProductCommand(int id)
    {
        Id = id;
    }
}
