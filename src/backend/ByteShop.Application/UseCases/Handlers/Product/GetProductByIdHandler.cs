using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class GetProductByIdHandler : IHandler<GetByIdCommand, ProductDTO>
{
    private readonly IProductRepository _productRepo;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IProductRepository productRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _mapper = mapper;
    }

    public async Task<RequestResult<ProductDTO>> Handle(GetByIdCommand command)
    {
        var productEntity = await _productRepo.GetByIdAsync(command.Id);   

        if(productEntity is null)
            return new RequestResult<ProductDTO>().NotFound();

        var productDTO = _mapper.Map<ProductDTO>(productEntity);

        return new RequestResult<ProductDTO>().Ok(productDTO);
    }
}
