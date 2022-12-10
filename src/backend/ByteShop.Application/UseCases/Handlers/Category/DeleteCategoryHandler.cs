using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;

namespace ByteShop.Application.UseCases.Handlers.Category;
public class DeleteCategoryHandler : IHandler<IdCommand, object>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;

    public DeleteCategoryHandler(
        ICategoryRepository categoryRepo,
        IUnitOfWork uow)
    {
        _categoryRepo = categoryRepo;
        _uow = uow;
    }

    public async Task<RequestResult<object>> Handle(IdCommand command)
    {
        var category = await _categoryRepo.GetByIdWithProductsAsync(command.Id);

        if (category.Products.Any())
            throw new ByteShopException(
                ResourceErrorMessages.THERE_IS_A_PRODUCT_ASSOCIATED_WITH_THE_CATEGORY);

        _categoryRepo.Remove(category.Id);
        await _uow.CommitAsync();

        return new RequestResult<object>().Accepted();
    }
}
