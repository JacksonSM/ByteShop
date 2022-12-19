using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions.Exceptions;
using ByteShop.Exceptions;
using ByteShop.Application.Services;
using static System.Net.Mime.MediaTypeNames;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class UpdateProductHandler : IHandler<UpdateProductCommand, ProductDTO>
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public UpdateProductHandler(
        IProductRepository productRepo,
        ICategoryRepository categoryRepo,
        IUnitOfWork uow,
        IMapper mapper,
        IImageService imageService)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _uow = uow;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<RequestResult<ProductDTO>> Handle(UpdateProductCommand command)
    {
        var product = await _productRepo.GetByIdAsync(command.Id);
        if (product == null)
            return new RequestResult<ProductDTO>().NotFound();

        await ValidateAsync(command, product);

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
                categoryId: command.CategoryId
            );

        _productRepo.Update(product);
        await _uow.CommitAsync();

        var produtcDTO = _mapper.Map<ProductDTO>(product);
        return new RequestResult<ProductDTO>().Ok(produtcDTO);
    }

    private async Task ValidateAsync(UpdateProductCommand command,
        Domain.Entities.Product product)
    {
        var validator = new UpdateProductValidation();
        var validationResult = validator.Validate(command);

        var IsThereCategory = await _categoryRepo.ExistsById(command.CategoryId);
        if (!IsThereCategory) validationResult.Errors
                .Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));

        if (command.AreThereImagesToAdd())
        {
            var finalAmount = FinalAmountOfImages(command, product);
            if (finalAmount > 5) validationResult.Errors
                .Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                ResourceErrorMessages.MAXIMUM_AMOUNT_OF_IMAGES));
        }

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }

    private int FinalAmountOfImages(UpdateProductCommand command,
        Domain.Entities.Product product)
    {
        // refatorar o nome
        var result = product.GetImagesTotal() - command.GetTotalImagesToRemove();
        return result + command.GetTotalImagesToAdd();
    }
}
