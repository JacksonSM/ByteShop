using ByteShop.Infra.CrossCutting.Bus;

namespace ByteShop.Application.Commands.Category;
public class UpdateCategoryCommand : Command
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }

    public void SetId(int id)
    {
        Id = id;
    }

    public override bool IsValid()
    {
        return ValidationResult.IsValid;
    }
}
