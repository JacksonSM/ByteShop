using ByteShop.Application.Commands.Product;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Domain.Interfaces.Services;
using ByteShop.Exceptions;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.CommandHandlers.Product;
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IImageService _imageService;

    public UpdateProductHandler(
        IProductRepository productRepo,
        ICategoryRepository categoryRepo,
        IUnitOfWork uow,
        IImageService imageService)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _uow = uow;
        _imageService = imageService;
    }

    public async Task<ValidationResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepo.GetByIdAsync(command.Id);
        var IsThereCategory = await _categoryRepo.ExistsById(command.CategoryId);

        var commandValidationResult = command.Validate(product, IsThereCategory);
        if (!commandValidationResult.IsValid)
            return commandValidationResult;

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

        if (product.IsValid())
        {
            await RemoveImages(command, product);
            await UploadImages(command, product);
            _productRepo.Update(product);
            await _uow.CommitAsync();
        }

        return product.ValidationResult;
    }

    private async Task RemoveImages(UpdateProductCommand command, Domain.Entities.Product product)
    {
        if (command.SetMainImageBase64 is not null)
        {
            await _imageService.DeleteImageAsync(product.MainImageUrl);
        }


        if (command.RemoveSecondaryImageUrl?.Length > 0)
        {
            await _imageService.DeleteImageAsync(command.RemoveSecondaryImageUrl);
            foreach (var url in command.RemoveSecondaryImageUrl)
            {
                product.RemoveSecondaryImage(url);
            }
        }
    }

    private async Task UploadImages(UpdateProductCommand command, Domain.Entities.Product product)
    {
        if (command.SetMainImageBase64 is not null)
        {
            var mainImageUrl = await _imageService.UploadBase64ImageAsync(command.SetMainImageBase64.Base64,
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
    }
}


