using ByteShop.Domain.DomainMessages;
using ByteShop.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.Product.RemoveProduct;
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepo;
    private readonly IUnitOfWork _uow;

    public DeleteProductHandler(IProductRepository productRepo, IUnitOfWork uow)
    {
        _productRepo = productRepo;
        _uow = uow;
    }

    public async Task<ValidationResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepo.GetByIdAsync(command.Id);

        if (product is null)
            product.AddValidationError("Id", ResourceValidationErrorMessage.PRODUCT_DOES_NOT_EXIST);

        if (product.IsValid())
        {
            product.Disable();
            _productRepo.Update(product);
            await _uow.CommitAsync();
        }
        return product.ValidationResult;
    }
}
