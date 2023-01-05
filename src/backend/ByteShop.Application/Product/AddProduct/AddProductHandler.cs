using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Domain.Interfaces.Services;
using MediatR;


namespace ByteShop.Application.Product.AddProduct;
public class AddProductHandler : IRequestHandler<AddProductCommand, AddProductResponse>
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IUnitOfWork _uow;
    private readonly IImageService _imageService;

    public AddProductHandler(
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

    public async Task<AddProductResponse> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var IsThereCategory = await _categoryRepo.ExistsById(command.CategoryId);
        var commandValidationResult = command.Validate(IsThereCategory);
        if (!commandValidationResult.IsValid)
            return new AddProductResponse(commandValidationResult);

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

        if (newProduct.IsValid())
        {
            await UploadImages(command, newProduct);
            await _productRepo.AddAsync(newProduct);
            await _uow.CommitAsync();
        }
        return new AddProductResponse(newProduct.Id, newProduct.ValidationResult);
    }

    private async Task UploadImages(AddProductCommand command, Domain.Entities.Product newProduct)
    {
        if (command.MainImageBase64 is not null)
        {
            var mainImageUrl = await _imageService.UploadBase64ImageAsync(command.MainImageBase64.Base64,
                command.MainImageBase64.Extension);

            newProduct.SetMainImage(mainImageUrl);
        }

        if (command.SecondaryImagesBase64?.Length > 0)
        {
            foreach (var imageBase64 in command.SecondaryImagesBase64)
            {
                var url = await _imageService.UploadBase64ImageAsync(imageBase64.Base64,
                    imageBase64.Extension);
                newProduct.AddSecondaryImage(url);
            }
        }
    }
}
