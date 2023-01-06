using ByteShop.Application.Configuration.Command;
using FluentValidation.Results;
namespace ByteShop.Application.Category.UpdateCategory;
public class UpdateCategoryCommand : CommandBase<ValidationResult>
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }

    public void SetId(int id)
    {
        Id = id;
    }

    public bool IsValid()
    {
        return ValidationResult.IsValid;
    }
}
