using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class AddProductHandler : IHandler<AddProductCommand, ProductDTO>
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AddProductHandler(
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

    public async Task<RequestResult<ProductDTO>> Handle(AddProductCommand command)
    {
        await ValidateAsync(command);

        var newProduct = new Domain.Entities.Product
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
                heigth: command.Heigth,
                lenght: command.Lenght,
                categoryId: command.CategoryId
            );

        if(!string.IsNullOrEmpty(command.MainImageUrl))
            newProduct.SetMainImage(command.MainImageUrl);

        //Receber url imagem real
        if(!string.IsNullOrEmpty(command.SecondaryImageUrl))
            newProduct.SetSecondaryImage(Array.Empty<string>());

        await _productRepo.AddAsync(newProduct);
        await _uow.CommitAsync();

        var produtcDTO = _mapper.Map<ProductDTO>(newProduct);

        return new RequestResult<ProductDTO>().Created(produtcDTO);
    }

    private async Task ValidateAsync(AddProductCommand command)
    {
        var validator = new AddProductValidation();
        var validationResult = validator.Validate(command);

        var IsThereCategory = await _categoryRepo.ExistsById(command.CategoryId);

        if (!IsThereCategory) validationResult.Errors
                .Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
