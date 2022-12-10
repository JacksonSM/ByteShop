using ByteShop.Domain.Entities;

namespace ByteShop.Application.DTOs;
public class CategoryWithAssociationDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }

    public CategoryDTO ParentCategory { get; private set; }
    public CategoryDTO[] ChildCategories { get; private set; }
}
