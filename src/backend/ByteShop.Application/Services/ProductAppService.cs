using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.Product.AddProduct;
using ByteShop.Application.Product.GetAllProducts;
using ByteShop.Application.Product.RemoveProduct;
using ByteShop.Application.Product.UpdateProduct;
using ByteShop.Application.Services.Contracts;
using ByteShop.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.Services;
public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepo;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductAppService(
        IProductRepository repoProduct, 
        IMediator mediator,
        IMapper mapper)
    {
        _productRepo = repoProduct;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<AddProductResponse> Add(AddProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
    public async Task<ValidationResult> Update(UpdateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<ValidationResult> Delete(DeleteProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }


    public async Task<GetAllProductsResponse> GetAll(GetAllProductsQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }

    public async Task<ProductDTO> GetById(int id)
    {
        var category = await _productRepo.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(category);
    }

}
