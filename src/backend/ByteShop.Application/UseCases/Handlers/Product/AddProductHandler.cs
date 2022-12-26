using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.Services;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
using Microsoft.Extensions.Logging;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class AddProductHandler : IHandler<AddProductCommand, ProductDTO>
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private readonly ILogger<AddProductHandler> _logger;

    public AddProductHandler(
        IProductRepository productRepo,
        ICategoryRepository categoryRepo,
        IUnitOfWork uow,
        IMapper mapper,
        IImageService imageService,
        ILogger<AddProductHandler> logger)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _uow = uow;
        _mapper = mapper;
        _imageService = imageService;
        _logger = logger;
    }

    public async Task<RequestResult<ProductDTO>> Handle(AddProductCommand command)
    {
        _logger.LogInformation("Entered the add product handler");

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
                height: command.Height,
                length: command.Length,
                width: command.Width,
                categoryId: command.CategoryId
            );

        if (command.MainImageBase64 is not null)
        {
            _logger.LogInformation("Uploading main image...");
            var mainImageUrl = await _imageService.UploadBase64ImageAsync(command.MainImageBase64.Base64,
                command.MainImageBase64.Extension);

            _logger.LogDebug("Generated link: {@mainImageUrl}", mainImageUrl);
            newProduct.SetMainImage(mainImageUrl);
        }

        if (command.SecondaryImagesBase64.Length > 0)
        {
            _logger.LogInformation("Uploading secondary image...");
            foreach (var imageBase64 in command.SecondaryImagesBase64)
            {
                var url = await _imageService.UploadBase64ImageAsync(imageBase64.Base64,
                    imageBase64.Extension);
                _logger.LogDebug("Generated link: {@url}", url);
                newProduct.AddSecondaryImage(url);
            }
        }

        await _productRepo.AddAsync(newProduct);
        await _uow.CommitAsync();

        _logger.LogInformation("Entity has been saved to the database {@newProduct}", newProduct);
        var produtcDTO = _mapper.Map<ProductDTO>(newProduct);
        return new RequestResult<ProductDTO>().Created(produtcDTO);
    }

    private async Task ValidateAsync(AddProductCommand command)
    {
        var validator = new AddProductValidation();
        var validationResult = validator.Validate(command);

        var IsThereCategory = await _categoryRepo.ExistsById(command.CategoryId);
        if (!IsThereCategory) validationResult.Errors
                .Add(new FluentValidation.Results
                .ValidationFailure(string.Empty, ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));


        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            _logger.LogInformation("A validation error occurred: {@errorMessages}", errorMessages);
            _logger.LogDebug("Command: {@command}", command);
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
