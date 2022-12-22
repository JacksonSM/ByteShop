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

        if (command.SetMainImageBase64 is not null)
            await _imageService.DeleteImageAsync(product.MainImageUrl);

        if (command.RemoveSecondaryImageUrl?.Length > 0)
        {
            await _imageService.DeleteImageAsync(command.RemoveSecondaryImageUrl);
            foreach (var url in command.RemoveSecondaryImageUrl)
            {
                product.RemoveSecondaryImage(url);
            }
        }

        if (command.SetMainImageBase64 != null)
        {
            var mainImageUrl = await _imageService.UploadBase64ImageAsync(
                command.SetMainImageBase64.Base64,
                command.SetMainImageBase64.Extension);

            product.SetMainImage(mainImageUrl);
        }

        if (command.AddSecondaryImageBase64?.Length > 0)
        {
            foreach (var imageBase64 in command.AddSecondaryImageBase64)
            {
                var url = await _imageService.UploadBase64ImageAsync(imageBase64.Base64,
                    imageBase64.Extension);
                product.AddSecondaryImage(url);
            }
        }

        _productRepo.Update(product);
        await _uow.CommitAsync();

        var produtcDTO = _mapper.Map<ProductDTO>(product);
        return new RequestResult<ProductDTO>().Ok(produtcDTO);
    }

    private async Task ValidateAsync(UpdateProductCommand command,
        Domain.Entities.Product product)
    {
        var validator = new UpdateProductValidation(product);
        var validationResult = validator.Validate(command);

        var IsThereCategory = await _categoryRepo.ExistsById(command.CategoryId);
        if (!IsThereCategory) validationResult.Errors
                .Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));


        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }

    }
}


