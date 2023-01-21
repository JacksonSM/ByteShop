using AutoMapper;
using ByteShop.Application.Category.AddCategory;
using ByteShop.Application.Category.RemoveCategory;
using ByteShop.Application.Category.UpdateCategory;
using ByteShop.Application.DTOs;
using ByteShop.Application.Services.Contracts;
using ByteShop.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.Services;

public class CategoryAppService : ICategoryAppService
{
    private readonly ICategoryRepository _repo;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository repo, IMediator mediator, IMapper mapper)
    {
        _repo = repo;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ValidationResult> Add(AddCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    public async Task<ValidationResult> Update(UpdateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    public  async Task<ValidationResult> Delete(DeleteCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<CategoryDTO> GetById(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        return _mapper.Map<CategoryDTO>(category);  
    }

    public async Task<CategoryDTO[]> GetAll()
    {
        var categories = await _repo.GetAllAsync();
        return _mapper.Map<CategoryDTO[]>(categories);  
    }



}
