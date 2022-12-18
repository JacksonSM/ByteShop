using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions.Exceptions;
using ByteShop.Exceptions;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class UpdateProductHandler : IHandler<UpdateProductCommand, ProductDTO>
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UpdateProductHandler(
        IProductRepository productRepo,
        ICategoryRepository categoryRepo, 
        IUnitOfWork uow,
        IMapper mapper)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<RequestResult<ProductDTO>> Handle(UpdateProductCommand command)
    {
        await ValidateAsync(command);

        var product = await _productRepo.GetByIdAsync(command.Id);
        if (product == null)
            return new RequestResult<ProductDTO>().NotFound();

        product.Update
            (
                name: command.Name,
                description: command.Description,
                price: command.Price,
                sku: command.SKU,
                costPrice: command.CostPrice,
                stock: command.Stock,
                warranty: command.Warranty,
                brand: command.Brand,
                weight: command.Weight,
                height: command.Height,
                length: command.Length,
                width: command.Width,
                categoryId: command.CategoryId,
                mainImageUrl: command.MainImageUrl,
                secondaryImageUrl: command.SecondaryImageUrl
            );
        
        _productRepo.Update(product);
        await _uow.CommitAsync();

        var produtcDTO = _mapper.Map<ProductDTO>(product);
        return new RequestResult<ProductDTO>().Ok(produtcDTO);
    }

    private async Task ValidateAsync(UpdateProductCommand command)
    {
        var validator = new UpdateProductValidation();
        var validationResult = validator.Validate(command);

        if (command.CategoryId != 0)
        {
            var IsThereCategory = await _categoryRepo.ExistsById(command.CategoryId);

            if (!IsThereCategory) validationResult.Errors
                    .Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                    ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));
        }
        

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
