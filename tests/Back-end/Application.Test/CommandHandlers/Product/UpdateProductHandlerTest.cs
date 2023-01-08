using ByteShop.Application.Product.UpdateProduct;
using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Entities;
using Utilities.Mapper;
using Utilities.Repositories;
using Utilities.Services;
using Xunit;

namespace Application.Test.CommandHandlers.Product;
public class UpdateProductHandlerTest
{
    [Fact]
    public async void Sucesso()
    {
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        var productToUpdate = ProductBuilder.ProductBuild();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        command.SetId(productToUpdate.Id);
        command.CategoryId = category.Id;
        var handler = CreateUpdateProductHandler(category, productToUpdate);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async void CategoriaInexistente()
    {
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        var productToUpdate = ProductBuilder.ProductBuild();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        command.SetId(productToUpdate.Id);
        command.CategoryId = category.Id + 3;
        var handler = CreateUpdateProductHandler(category, productToUpdate);
        CancellationTokenSource cts = new CancellationTokenSource();

        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeFalse();
        response.Errors.Any(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST))
            .Should().BeTrue();
    }

    [Fact]
    public async void ImageServiceRecebendoDadosCorretos()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        command.SetId(productToUpdate.Id);
        command.CategoryId = category.Id;
        var productRepo = ProductRepositoryBuilder.Instance().SetupGetById(productToUpdate).Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupGetById(category).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .GetMock();

        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<UpdateProductHandler>.Instance().Build();

        var handler = new UpdateProductHandler(productRepo, categoryRepo, uow, imageService.Object);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeTrue();

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
    public async void OperacaoOKProdutoComNenhumaImagem()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            changeMainImage: false,
            numberOfSecondaryImagesToAdd: 0,
            numberOfSecondaryImagesToRemove: 0);
        command.SetId(productToUpdate.Id);
        command.CategoryId = category.Id;
        var handler = CreateUpdateProductHandler(category, productToUpdate);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async void ProdutoApenasComAImagemPrincipal()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.SetId(productToUpdate.Id);
        command.AddSecondaryImageBase64 = null;
        command.CategoryId = category.Id;
        var productRepo = ProductRepositoryBuilder.Instance().SetupGetById(productToUpdate).Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupGetById(category).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .GetMock();

        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<UpdateProductHandler>.Instance().Build();

        var handler = new UpdateProductHandler(productRepo, categoryRepo, uow, imageService.Object);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);
        response.IsValid.Should().BeTrue();
        imageService
            .Verify(m => m.UploadBase64ImageAsync(command.SetMainImageBase64.Base64,
                command.SetMainImageBase64.Extension), Moq.Times.Once);
    }

    [Fact]
    public async void ProdutoInexistente()
    {
        var productToUpdate = ProductBuilder.ProductBuild();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        productToUpdate.Id = 3;
        command.SetId(5);
        var handler = CreateUpdateProductHandler(category, productToUpdate);
        CancellationTokenSource cts = new CancellationTokenSource();

        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeFalse();
    }

    private static UpdateProductHandler CreateUpdateProductHandler(
        ByteShop.Domain.Entities.Category category,
        ByteShop.Domain.Entities.Product product)
    {
        var productRepo = ProductRepositoryBuilder.Instance().SetupGetById(product).Build();
        var categoryRepo = CategoryRepositoryBuilder.Instance().SetupGetById(category).Build();
        var imageService = ImageServiceBuilder.Instance()
            .SetupUpload()
            .Build();

        var uow = UnitOfWorkBuilder.Instance().Build();

        return new UpdateProductHandler(productRepo, categoryRepo, uow, imageService);
    }
}
