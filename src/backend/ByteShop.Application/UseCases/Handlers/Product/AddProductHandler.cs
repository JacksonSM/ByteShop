using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.Services;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class AddProductHandler : IHandler<AddProductCommand, ProductDTO>
{
    private const int MAXIMUM_AMOUNT_OF_IMAGES = 5;

    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public AddProductHandler(
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
                height: command.Height,
                length: command.Length,
                width: command.Width,
                categoryId: command.CategoryId
            );

        var mainImageUrl = await _imageService.UploadBase64ImageAsync(command.MainImageBase64.Base64,
            command.MainImageBase64.Extension);

        newProduct.SetMainImage(mainImageUrl);
        foreach (var imageBase64 in command.SecondaryImagesBase64)
        {
            var url = await _imageService.UploadBase64ImageAsync(imageBase64.Base64,
                imageBase64.Extension);
            newProduct.AddSecondaryImage(url);
        }

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
                .Add(new FluentValidation.Results
                .ValidationFailure(string.Empty, ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));

        if(command.TotalImages() > MAXIMUM_AMOUNT_OF_IMAGES)
            validationResult.Errors
            .Add(new FluentValidation.Results
            .ValidationFailure(string.Empty, ResourceErrorMessages.MAXIMUM_AMOUNT_OF_IMAGES));

        if (command.TotalImages() > 1 && !command.MainImageHasItBeenDefined())
        {
            validationResult.Errors
            .Add(new FluentValidation.Results
                .ValidationFailure(string.Empty, ResourceErrorMessages.MUST_HAVE_A_MAIN_IMAGE));
        }

        if(command.MainImageBase64 != null)
        {
            var mainImageIsValid = _imageService.ItsValid(command.MainImageBase64.Base64,
                command.MainImageBase64.Extension);

            if (!string.IsNullOrEmpty(mainImageIsValid)) validationResult.Errors
                .Add(new FluentValidation.Results
                .ValidationFailure(string.Empty, mainImageIsValid));
        }

        if (command.SecondaryImagesBase64?.Length > 0)
        {
            var imagesListIsValid = _imageService.ItsValid(command.SecondaryImagesBase64);

            if (!string.IsNullOrEmpty(imagesListIsValid)) validationResult.Errors
                .Add(new FluentValidation.Results
                .ValidationFailure(string.Empty, imagesListIsValid));
        }


        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
