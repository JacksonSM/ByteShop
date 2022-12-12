using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;

namespace ByteShop.Application.UseCases.Handlers.Category;
public class GetAllCategoryHandler : IHandler<NoParametersCommand, CategoryDTO[]>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public GetAllCategoryHandler(ICategoryRepository categoryRepo, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    public async Task<RequestResult<CategoryDTO[]>> Handle(NoParametersCommand command)
    {
        var categories = await _categoryRepo.GetAllAsync();

        if (categories.Count == 0)
            return new RequestResult<CategoryDTO[]>().NoContext();

        var categoriesDTOs = _mapper.Map<CategoryDTO[]>(categories);
        return new RequestResult<CategoryDTO[]>().Ok(categoriesDTOs);
    }
}
