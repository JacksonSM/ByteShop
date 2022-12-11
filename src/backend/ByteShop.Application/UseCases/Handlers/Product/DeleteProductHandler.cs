using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Results;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class DeleteProductHandler : IHandler<IdCommand, object>
{
    private readonly IProductRepository _productRepo;
    private readonly IUnitOfWork _uow;

    public DeleteProductHandler(IProductRepository productRepo, IUnitOfWork uow)
    {
        _productRepo = productRepo;
        _uow = uow;
    }

    public async Task<RequestResult<object>> Handle(IdCommand command)
    {
        var product = await _productRepo.GetByIdAsync(command.Id);

        if (product is null)
            throw new ByteShopException(
                ResourceErrorMessages.PRODUCT_DOES_NOT_EXIST);

        product.Disable();
        _productRepo.Update(product);
        await _uow.CommitAsync();

        return new RequestResult<object>().Accepted();
    }
}
