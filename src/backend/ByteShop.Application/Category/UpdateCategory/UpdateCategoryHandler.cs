using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.Category.UpdateCategory;
public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ValidationResult>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;

    public UpdateCategoryHandler(
        ICategoryRepository categoryRepo,
        IUnitOfWork uow)
    {
        _categoryRepo = categoryRepo;
        _uow = uow;
    }

    public async Task<ValidationResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoryRepo.GetByIdWithAssociationAsync(command.Id);
        if (category is null)
            command.AddValidationError("ID", ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST);

        if (!command.IsValid())
            return command.ValidationResult;

        Domain.Entities.Category newParentCategory = null;
        if (command.ParentCategoryId != 0)
        {
            newParentCategory = await _categoryRepo.GetByIdAsync(command.ParentCategoryId);

            if (newParentCategory is null)
                category.AddValidationError("ParentCategory", ResourceErrorMessages.PARENT_CATEGORY_DOES_NOT_EXIST);
        }

        category.Update(command.Name, newParentCategory);

        if (category.IsValid())
        {
            _categoryRepo.Update(category);
            await _uow.CommitAsync();
        }
        return category.ValidationResult;
    }
}
