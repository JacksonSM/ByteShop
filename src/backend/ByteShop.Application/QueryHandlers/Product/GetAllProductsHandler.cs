using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.Queries;
using ByteShop.Application.Reponses;
using ByteShop.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ByteShop.Application.QueryHandlers.Product;
public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsResponse>
{
    private readonly IProductRepository _productRepo;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IProductRepository productRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _mapper = mapper;
    }

    public async Task<GetAllProductsResponse> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        (IEnumerable<Domain.Entities.Product> products, int quantityProduct) =
            await _productRepo.GetAllAsync
        (
            sku: query.sku,
            name: query.name,
            brand: query.brand,
            category: query.category,
            actualPage: query.actualPage,
            itemsPerPage: query.itemsPerPage
        );

        var productsDTOs = _mapper.Map<ProductDTO[]>(products);
        var pagination = BuildPagination(query, quantityProduct);

        return new GetAllProductsResponse(productsDTOs, pagination);
    }

    public Pagination BuildPagination(GetAllProductsQuery query, int quantityProduct)
    {
        if (!query.actualPage.HasValue && !query.itemsPerPage.HasValue) return null;

        var pagination = new Pagination
        (
            actualPage: query.actualPage.Value,
            itemsPerPage: query.itemsPerPage.Value,
            itemsTotal: quantityProduct
        );

        return pagination;
    }
}
