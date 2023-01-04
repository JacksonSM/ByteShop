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
    public Category() {
    }

    public Category(string name)
    {
        Name = name;
        ParentCategoryId = null;
        CategoryLevel = CategoryLevel.LevelOne;
    }

    public Category(string name, Category parentCategory)
    {
        if (parentCategory is not null)
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

    public bool CanItBeDeleted()
    {
        if (Products.Any())
            AddValidationError("Products",
                ResourceDomainMessages.THERE_IS_A_PRODUCT_ASSOCIATED_WITH_THE_CATEGORY);

        if (ChildCategories.Any())
            AddValidationError("ChildCategories",
                ResourceDomainMessages.THERE_IS_AN_ASSOCIATED_CHILD_CATEGORY);

        return IsValid();
    }

    public override bool IsValid()
    {
        var validator = new CategoryValidation();
        var result = validator.Validate(this);
        ValidationResult.Errors.AddRange(result.Errors);
        return ValidationResult.IsValid;
    }
}
