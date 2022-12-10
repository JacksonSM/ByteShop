using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;

namespace ByteShop.Application.UseCases.Handlers.Category;
public class GetAllCategoryHandler : IHandler<NoParametersCommand, CategoryWithAssociationDTO[]>
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public GetAllCategoryHandler(ICategoryRepository categoryRepo, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    public async Task<RequestResult<CategoryWithAssociationDTO[]>> Handle(NoParametersCommand command)
    {
        var categories = await _categoryRepo.GetAllAsync();

        if (categories.Count == 0)
            return new RequestResult<CategoryWithAssociationDTO[]>().NoContext();

        var categoriesDTOs = _mapper.Map<CategoryWithAssociationDTO[]>(categories);
        return new RequestResult<CategoryWithAssociationDTO[]>().Ok(categoriesDTOs);
    }
}
