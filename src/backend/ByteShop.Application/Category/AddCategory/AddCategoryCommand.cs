using ByteShop.Application.Configuration.Command;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.Category.AddCategory;
public class AddCategoryCommand : CommandBase<ValidationResult> 
{
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }

    public override bool IsValid()
    {
        return Errors.IsValid;
    }
}
