using ByteShop.Application.UseCases.Handlers.Product;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Entities;
using Utilities.Mapper;
using Utilities.Repositories;
using Utilities.Services;
using Xunit;

namespace Handlers.Test.Product;
public class UpdateProductHandlerTest
{
    [Fact]
    public async void Sucesso()
    {
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        var productToUpdate = ProductBuilder.ProductBuild();
        command.SetId(productToUpdate.Id);
        var handler = CreateUpdateProductHandler(command.CategoryId, productToUpdate);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(200);
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
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        var productToUpdate = ProductBuilder.ProductBuild();
        command.SetId(productToUpdate.Id);
        command.CategoryId = 0;
        var handler = CreateUpdateProductHandler(command.CategoryId, productToUpdate);

        Func<Task> action = async () => { await handler.Handle(command); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 &&
            exception.ErrorMessages.Contains(ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));
    }

    [Fact]
    public async void ImageServiceRecebendoDadosCorretos()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.SetId(productToUpdate.Id);
        var productRepo = ProductRepositoryBuilder.Instance().SetupGetById(productToUpdate).Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupExistById(command.CategoryId).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .GetMock();

        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();

        var handler = new UpdateProductHandler(productRepo, categoryRepo, uow, mapper, imageService.Object);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(200);
        imageService
            .Verify(m => m.UploadBase64ImageAsync(command.SetMainImageBase64.Base64,
                command.SetMainImageBase64.Extension));

        foreach (var image in command.AddSecondaryImageBase64)
        {
            imageService
                .Verify(m => m.UploadBase64ImageAsync(image.Base64,
                    image.Extension));
        }
    }
    [Fact]
    public async void ProdutoComNenhumaImagem()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            changeMainImage: false,
            numberOfSecondaryImagesToAdd: 0,
            numberOfSecondaryImagesToRemove: 0);
        command.SetId(productToUpdate.Id);
        var handler = CreateUpdateProductHandler(command.CategoryId, productToUpdate);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async void ProdutoApenasComAImagemPrincipal()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.SetId(productToUpdate.Id);
        var productRepo = ProductRepositoryBuilder.Instance().SetupGetById(productToUpdate).Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupExistById(command.CategoryId).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .GetMock();

        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();

        var handler = new UpdateProductHandler(productRepo, categoryRepo, uow, mapper, imageService.Object);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(200);
        imageService
            .Verify(m => m.UploadBase64ImageAsync(command.SetMainImageBase64.Base64,
                command.SetMainImageBase64.Extension), Moq.Times.Once);
    }

    [Fact]
    public async void ProdutoInexistente()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        productToUpdate.Id = 3;
        command.SetId(5);
        var handler = CreateUpdateProductHandler(command.CategoryId, productToUpdate);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(404);
    }

    private static UpdateProductHandler CreateUpdateProductHandler(int categoryId,
        ByteShop.Domain.Entities.Product product)
    {
        var productRepo = ProductRepositoryBuilder.Instance().SetupGetById(product).Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupExistById(categoryId).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .Build();

        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();

        return new UpdateProductHandler(productRepo, categoryRepo, uow, mapper, imageService);
    }
}
