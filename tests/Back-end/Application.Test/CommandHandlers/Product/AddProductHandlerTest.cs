using ByteShop.Application.Product.AddProduct;
using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Mapper;
using Utilities.Repositories;
using Utilities.Services;
using Xunit;

namespace Application.Test.CommandHandlers.Product;
public class AddProductHandlerTest
{
    [Fact]
    public async void Sucesso()
    {
        var command = ProductCommandBuilder.AddProductCommandBuild();
        var handler = CreateAddProductHandler(command.CategoryId);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.ValidationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public async void CategoriaInexistente()
    {
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.CategoryId = 0;
        var handler = CreateAddProductHandler(command.CategoryId);
        CancellationTokenSource cts = new CancellationTokenSource();

        var response = await handler.Handle(command, cts.Token);

        response.ValidationResult.IsValid.Should().BeFalse();
        response.ValidationResult.Errors.Any(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST))
            .Should().BeTrue();
    }

    [Fact]
    public async void ImageServiceRecebendoDadosCorretos()
    {
        var command = ProductCommandBuilder.AddProductCommandBuild();
        var productRepo = ProductRepositoryBuilder.Instance().Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupExistById(command.CategoryId).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .GetMock();

        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<AddProductHandler>.Instance().Build();

        var handler = new AddProductHandler(productRepo, categoryRepo, uow, imageService.Object);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.ValidationResult.IsValid.Should().BeTrue();
        imageService
            .Verify(m => m.UploadBase64ImageAsync(command.MainImageBase64.Base64,
                command.MainImageBase64.Extension));

        foreach (var image in command.SecondaryImagesBase64)
        {
            imageService
                .Verify(m => m.UploadBase64ImageAsync(image.Base64,
                    image.Extension));
        }
    }
    [Fact]
    public async void ProdutoComNenhumaImagem()
    {
        var command = ProductCommandBuilder.AddProductCommandBuild(numberSecondaryImages: 0);
        command.MainImageBase64 = null;
        var handler = CreateAddProductHandler(command.CategoryId);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.ValidationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public async void AdicionarProdutoApenasComAImagemPrincipal()
    {
        var command = ProductCommandBuilder.AddProductCommandBuild();
        var productRepo = ProductRepositoryBuilder.Instance().Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupExistById(command.CategoryId).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .GetMock();

        command.SecondaryImagesBase64 = null;

        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<AddProductHandler>.Instance().Build();

        var handler = new AddProductHandler(productRepo, categoryRepo, uow, imageService.Object);
        CancellationTokenSource cts = new CancellationTokenSource();

        var response = await handler.Handle(command, cts.Token);

        response.ValidationResult.IsValid.Should().BeTrue();
        imageService
            .Verify(m => m.UploadBase64ImageAsync(command.MainImageBase64.Base64,
                command.MainImageBase64.Extension), Moq.Times.Once);
    }

    private static AddProductHandler CreateAddProductHandler(int categoryId)
    {
        var productRepo = ProductRepositoryBuilder.Instance().Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupExistById(categoryId).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .Build();

        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<AddProductHandler>.Instance().Build();

        return new AddProductHandler(productRepo, categoryRepo, uow, imageService);

    }
}
