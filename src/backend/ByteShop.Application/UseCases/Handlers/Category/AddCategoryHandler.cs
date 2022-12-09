using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Category;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Category;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;

namespace ByteShop.Application.UseCases.Handlers.Category;
public class AddCategoryHandler : IHandler<AddCategoryCommand, CategoryDTO>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AddCategoryHandler(
        ICategoryRepository categoryRepo,
        IUnitOfWork uow,
        IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<RequestResult<CategoryDTO>> Handle(AddCategoryCommand command)
    {
        Domain.Entities.Category parentCategory = null;

        if(command.ParentCategoryId != 0)
            parentCategory = await _categoryRepo
                .GetByIdWithAssociation(command.ParentCategoryId);

        Validate(command, parentCategory);

        var category = new Domain.Entities.Category(command.Name);

        if (parentCategory != null)
        {
            parentCategory.AddChild(category);
            _categoryRepo.Update(parentCategory);
        }
        else
        {
            await _categoryRepo.AddAsync(category);
        }
        await _uow.CommitAsync();

        var categoryDTO = _mapper.Map<CategoryDTO>(category);
        return new RequestResult<CategoryDTO>().Created(categoryDTO);   
    }

    private void Validate(AddCategoryCommand command,
        Domain.Entities.Category parentCategory)
    {
        var validator = new AddCategoryValidation();
        var validationResult = validator.Validate(command);

        if(parentCategory is null && command.ParentCategoryId != 0)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty, ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
