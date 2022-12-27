using ByteShop.Application.UseCases.Handlers.Product;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Mapper;
using Utilities.Repositories;
using Utilities.Services;
using Xunit;

namespace Handlers.Test.Product;
public class AddProductHandlerTest
{
    [Fact]
    public async void Sucesso()
    {
        var command = ProductCommandBuilder.AddProductCommandBuild();
        var handler = CreateAddProductHandler(command.CategoryId);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(201);
        var productResponse = response.Data;

        productResponse.Name.Should().Be(command.Name);
        productResponse.Description.Should().Be(command.Description);
        productResponse.Brand.Should().Be(command.Brand);
        productResponse.SKU.Should().Be(command.SKU);
        productResponse.Price.Should().Be(command.Price);
        productResponse.CostPrice.Should().Be(command.CostPrice);
        productResponse.Warranty.Should().Be(command.Warranty);
        productResponse.Length.Should().Be(command.Length);
        productResponse.Height.Should().Be(command.Height);
        productResponse.Width.Should().Be(command.Width);
        productResponse.Weight.Should().Be(command.Weight);
        productResponse.CategoryId.Should().Be(command.CategoryId);
    }

    [Fact]
    public async void CategoriaInexistente()
    {
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.CategoryId = 0;
        var handler = CreateAddProductHandler(command.CategoryId);

        Func<Task> action = async () => { await handler.Handle(command); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 &&
            exception.ErrorMessages.Contains(ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));
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

        var handler = new AddProductHandler(productRepo, categoryRepo, uow, mapper, imageService.Object, logger);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(201);
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
        var command = ProductCommandBuilder.AddProductCommandBuild(numberSecondaryImages:0);
        command.MainImageBase64 = null;
        var handler = CreateAddProductHandler(command.CategoryId);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(201);
    }

    [Fact]
    public async void ProdutoApenasComAImagemPrincipal()
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
        var handler = new AddProductHandler(productRepo, categoryRepo, uow, mapper, imageService.Object, logger);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(201);
        imageService
            .Verify(m => m.UploadBase64ImageAsync(command.MainImageBase64.Base64,
                command.MainImageBase64.Extension),Moq.Times.Once);
    }

    private static AddProductHandler CreateAddProductHandler(int categoryId)
    {
        var productRepo = ProductRepositoryBuilder.Instance().Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupExistById(categoryId).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .Build();

        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<AddProductHandler>.Instance().Build();

        return new AddProductHandler(productRepo, categoryRepo, uow, mapper, imageService, logger);

    }
}
