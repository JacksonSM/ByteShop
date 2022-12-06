using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Category;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;

namespace ByteShop.Application.UseCases.Handlers.Category;
public class AddCategoryHandler : IHandler<AddCategoryCommand, CategoryDTO>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;

    public AddCategoryHandler(
        ICategoryRepository categoryRepo,
        IUnitOfWork uow)
    {
        _categoryRepo = categoryRepo;
        _uow = uow;
    }

    public async Task<RequestResult<CategoryDTO>> Handle(AddCategoryCommand command)
    {
        await ValidateAsync(command);
        return null;
    }

    private Task ValidateAsync(AddCategoryCommand command)
    {
        throw new NotImplementedException();
    }
}
