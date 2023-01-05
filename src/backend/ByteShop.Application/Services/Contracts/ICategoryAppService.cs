using ByteShop.Application.Category.AddCategory;
using ByteShop.Application.Category.RemoveCategory;
using ByteShop.Application.Category.UpdateCategory;
using ByteShop.Application.DTOs;
using FluentValidation.Results;

namespace ByteShop.Application.Services.Contracts;
public interface ICategoryAppService
{
    Task<ValidationResult> Add(AddCategoryCommand command);
    Task<ValidationResult> Update(UpdateCategoryCommand command);
    Task<ValidationResult> Delete(DeleteCategoryCommand command);

    Task<CategoryDTO> GetById(int id);
    Task<CategoryDTO[]> GetAll();
}
