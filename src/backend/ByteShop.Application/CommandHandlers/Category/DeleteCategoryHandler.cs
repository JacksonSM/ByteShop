using ByteShop.Application.Commands.Category;
using ByteShop.Domain.Entities;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.CommandHandlers.Category;
public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ValidationResult>
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

    public async Task<ValidationResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoryRepo.GetByIdWithChildCategoriesAndProductsAsync(command.Id);

        if (category is null)
            category.AddValidationError("Id", ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST);

        if (category.CanItBeDeleted())
        {
            _categoryRepo.Remove(category.Id);
            await _uow.CommitAsync();
        }

        return category.ValidationResult;
    }
}
