using AutoMapper;
using ByteShop.Application.Commands.Product;
using ByteShop.Application.DTOs;
using ByteShop.Application.Queries;
using ByteShop.Application.Reponses;
using ByteShop.Application.Services.Contracts;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infra.CrossCutting.Bus;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace ByteShop.Application.Services;
public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepo;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public ProductAppService(
        IProductRepository repoProduct, 
        IMediatorHandler mediator,
        IMapper mapper)
    {
        _productRepo = repoProduct;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ValidationResult> Add(AddProductCommand command)
    {
        var result = await _mediator.SendCommand(command);
        return result;
    }
    public async Task<ValidationResult> Update(UpdateProductCommand command)
    {
        var result = await _mediator.SendCommand(command);
        return result;
    }

    public async Task<ValidationResult> Delete(DeleteProductCommand command)
    {
        var result = await _mediator.SendCommand(command);
        return result;
    }


    public async Task<GetAllProductsResponse> GetAll(GetAllProductsQuery query)
    {
        var result = await _mediator.SendQuery(query);
        return result;
    }

    public async Task<ProductDTO> GetById(int id)
    {
        var category = await _productRepo.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(category);
    }

}
