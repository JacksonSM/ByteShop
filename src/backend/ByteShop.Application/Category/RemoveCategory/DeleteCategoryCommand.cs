using ByteShop.Application.Configuration.Command;
using FluentValidation.Results;
namespace ByteShop.Application.Category.RemoveCategory;
public class DeleteCategoryCommand : CommandBase<ValidationResult>
{
    public int Id { get; set; }

    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }
}
