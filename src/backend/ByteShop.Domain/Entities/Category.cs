using ByteShop.Domain.Emuns;
using ByteShop.Domain.Entities.Validations;
using ByteShop.Domain.Interfaces.Mediator;
using ByteShop.Exceptions;

namespace ByteShop.Domain.Entities;
public class Category : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public int? ParentCategoryId { get; private set; }
    public CategoryLevel CategoryLevel { get; private set; }
    public Category ParentCategory { get; private set; }
    public List<Category> ChildCategories { get; private set; }

    public List<Product> Products { get; private set; }

    //For EF
    public Category() { }

    public Category(string name)
    {
        Name = name;
        ParentCategoryId = null;
        CategoryLevel = CategoryLevel.LevelOne;
    }

    public Category(string name, Category parentCategory)
    {
        if(parentCategory is not null)
        {
            Name = name;
            ParentCategoryId = parentCategory.Id;
            CategoryLevel = parentCategory.CategoryLevel + 1;
        }
        else
        {
            AddValidationError("ParentCategoryId", ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST);
        }

    }

    public void Update(string name, Category newParentCategory)
    {
        Name = name;

        if (newParentCategory is not null)
        {
            var IsDifferent = ParentCategory.CategoryLevel != newParentCategory.CategoryLevel;
            if (IsDifferent)
            {
                AddValidationError("CategoryLevel", ResourceDomainMessages.MAXIMUM_CATEGORY_LEVEL);
                return;
            }

            ParentCategory = null;
            ParentCategoryId = newParentCategory.Id;
        }
    }

    public override bool IsValid()
    {
        var validator = new CategoryValidation();
        ValidationResult = validator.Validate(this);
        return ValidationResult.IsValid;
    }
}
