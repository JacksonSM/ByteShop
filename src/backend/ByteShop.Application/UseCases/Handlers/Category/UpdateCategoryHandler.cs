using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Category;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Category;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
using Microsoft.Extensions.Logging;

namespace ByteShop.Application.UseCases.Handlers.Category;
public class UpdateCategoryHandler : IHandler<UpdateCategoryCommand, CategoryDTO>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<AddCategoryHandler> _logger;
    public UpdateCategoryHandler(
        ICategoryRepository categoryRepo,
        IUnitOfWork uow,
        IMapper mapper,
        ILogger<AddCategoryHandler> logger)
    {
        _categoryRepo = categoryRepo;
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RequestResult<CategoryDTO>> Handle(UpdateCategoryCommand command)
    {
        _logger.LogInformation("Entered the update category handler");
        var category = await _categoryRepo.GetByIdAsync(command.Id);
        await ValidateAsync(command, category);
        _logger.LogInformation("category before updating: {@category}", category);

        category.Update(command.Name, command.ParentCategoryId);
        _categoryRepo.Update(category);
        await _uow.CommitAsync();

        _logger.LogInformation("successfully updated: {@category}", category);
        var categoryDTO = _mapper.Map<CategoryDTO>(category);
        return new RequestResult<CategoryDTO>().Ok(categoryDTO);
    }
    private async Task ValidateAsync(UpdateCategoryCommand command, 
        Domain.Entities.Category category)
    {
        var validator = new UpdateCategoryValidation();
        var validationResult = validator.Validate(command);

        if (category is null) validationResult.Errors
                .Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));

        if(command.ParentCategoryId != 0)
        {
            var IsThereParentCategory = await _categoryRepo.ExistsById(command.ParentCategoryId);

            if (!IsThereParentCategory) validationResult.Errors
                    .Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                    ResourceErrorMessages.PARENT_CATEGORY_DOES_NOT_EXIST));
        }

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            _logger.LogInformation("A validation error occurred: {@errorMessages}", errorMessages);
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
