using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;

namespace ByteShop.Application.UseCases.Handlers.Category;
public class GetCategoryByIdHandler : IHandler<IdCommand, CategoryDTO>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepo, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    public async Task<RequestResult<CategoryDTO>> Handle(IdCommand command)
    {
        var categoryEntity = await _categoryRepo.GetByIdAsync(command.Id);

        if(categoryEntity is null)
            return new RequestResult<CategoryDTO>().NotFound();

        var categoryDTO = _mapper.Map<CategoryDTO>(categoryEntity);

        return new RequestResult<CategoryDTO>().Ok(categoryDTO);
    }
}
