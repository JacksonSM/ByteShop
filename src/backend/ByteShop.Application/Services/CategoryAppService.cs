using AutoMapper;
using ByteShop.Application.Commands.Category;
using ByteShop.Application.DTOs;
using ByteShop.Application.Services.Contracts;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infra.CrossCutting.Bus;
using FluentValidation.Results;

namespace ByteShop.Application.Services;

public class CategoryAppService : ICategoryAppService
{
    private readonly ICategoryRepository _repo;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public CategoryAppService(ICategoryRepository repo, IMediatorHandler mediator, IMapper mapper)
    {
        _repo = repo;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ValidationResult> Add(AddCategoryCommand command)
    {
        var result = await _mediator.SendCommand(command);
        return result;
    }
    public async Task<ValidationResult> Update(UpdateCategoryCommand command)
    {
        var result = await _mediator.SendCommand(command);
        return result;
    }

    public  async Task<ValidationResult> Delete(DeleteCategoryCommand command)
    {
        var result = await _mediator.SendCommand(command);
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
