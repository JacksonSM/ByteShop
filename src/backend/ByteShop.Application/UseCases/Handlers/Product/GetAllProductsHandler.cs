using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;
using Newtonsoft.Json;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class GetAllProductsHandler : IHandler<GetAllProductsCommand, IEnumerable<ProductDTO>>
{
    private readonly IProductRepository _productRepo;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IProductRepository productRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _mapper = mapper;
    }

    public async Task<RequestResult<IEnumerable<ProductDTO>>> Handle(GetAllProductsCommand command)
    {

        (IEnumerable<Domain.Entities.Product> products , int QuantityProduct) =
            await _productRepo.GetAllAsync
            (
                sku: command.sku,
                name: command.name,
                brand: command.brand,
                category: command.category,
                actualPage: command.actualPage,
                itemsPerPage: command.itemsPerPage
            );

        if (!(products?.Count() > 0))
            return new RequestResult<IEnumerable<ProductDTO>>().NoContext();

        var produtcsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

        if (command.actualPage.HasValue && command.itemsPerPage.HasValue)
        {
            var pagination = new PaginationHeader
                (
                    actualPage: command.actualPage.Value,
                    itemsPerPage: command.itemsPerPage.Value,
                    itemsTotal: QuantityProduct
                );

            var paginationHeader = new Tuple<string, string>(pagination.Key,
                JsonConvert.SerializeObject(pagination));

            return new RequestResult<IEnumerable<ProductDTO>>().Ok(produtcsDTO, paginationHeader);
        }

        return new RequestResult<IEnumerable<ProductDTO>>().Ok(produtcsDTO);

    }
}
