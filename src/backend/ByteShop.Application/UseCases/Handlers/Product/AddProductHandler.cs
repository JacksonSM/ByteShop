using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Application.Services;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace ByteShop.Application.UseCases.Handlers.Product;
public class AddProductHandler : IHandler<AddProductCommand, ProductDTO>
{
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

        await _productRepo.AddAsync(newProduct);
        await _uow.CommitAsync();

        var mainImageUrl = await _imageService.UploadBase64ImageAsync(command.MainImageBase64.imageBase64,
            command.MainImageBase64.extension);

        newProduct.SetMainImage(mainImageUrl);
        foreach (var imageBase64 in command.SecondaryImagesBase64)
        {
            var url = await _imageService.UploadBase64ImageAsync(imageBase64.imageBase64,
                imageBase64.extension);
            newProduct.AddSecondaryImage(url);
        }

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

        var mainImageIsValid = _imageService.ItsValid(command.MainImageBase64.imageBase64,
            command.MainImageBase64.extension);

        if (!mainImageIsValid.Item1) validationResult.Errors
        .Add(new FluentValidation.Results
        .ValidationFailure(string.Empty, mainImageIsValid.Item2));

        List<Tuple<string, string>> imagesList = new();
        command.SecondaryImagesBase64.ToList().ForEach(x =>
        {
            imagesList.Add(new Tuple<string, string>(x.imageBase64, x.extension));
        });

        var imagesListIsValid = _imageService.ItsValid(imagesList.ToArray());

        if (!imagesListIsValid.Item1) validationResult.Errors
                .Add(new FluentValidation.Results
                .ValidationFailure(string.Empty, imagesListIsValid.Item2));

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
