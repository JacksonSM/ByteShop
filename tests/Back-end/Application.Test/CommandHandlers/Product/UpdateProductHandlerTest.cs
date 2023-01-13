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
    [Trait("Product", "Handler")]
    public async void UpdateProductHandler_WithValidData_ShouldReturnTrue()
    {
        //Arrange
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove:0,
            numberOfSecondaryImagesToAdd: 0);
        var productToUpdate = ProductBuilder.BuildProduct();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        command.SetId(productToUpdate.Id);
        command.CategoryId = category.Id;
        var handler = CreateUpdateProductHandler(category, productToUpdate);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "Handler")]
    public async void UpdateProductHandler_NonExistentCategory_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        var productToUpdate = ProductBuilder.BuildProduct();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        command.SetId(productToUpdate.Id);
        command.CategoryId = category.Id + 3;
        var handler = CreateUpdateProductHandler(category, productToUpdate);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeFalse();
        response.Errors.Any(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST))
            .Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "Handler")]
    public async void UploadBase64ImageAsync_WithValidData_ShouldReturnTrueAndTheServiceWillReceiveTheCorrectValues()
    {
        //Arrange
        var productToUpdate = ProductBuilder.BuildProduct();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);

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

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
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
    [Trait("Product", "Handler")]
    public async void UpdateProductHandler_NoImages_ShouldReturnTrue()
    {
        //Arrange
        var productToUpdate = ProductBuilder.BuildProduct();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            changeMainImage: false,
            numberOfSecondaryImagesToAdd: 0,
            numberOfSecondaryImagesToRemove: 0);
        command.SetId(productToUpdate.Id);
        command.CategoryId = category.Id;
        var handler = CreateUpdateProductHandler(category, productToUpdate);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "Handler")]
    public async void UpdateProductHandler_UpdateMainImageOnly_ShouldReturnTrueAndOldImageShouldBeDeleted()
    {
        //Arrange
        var productToUpdate = ProductBuilder.BuildProduct();
        var oldMainImage = productToUpdate.MainImageUrl;
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0);

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

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeTrue();
        imageService
            .Verify(m => m.UploadBase64ImageAsync(command.SetMainImageBase64.Base64,
                command.SetMainImageBase64.Extension), Moq.Times.Once);

        imageService
            .Verify(m => m.DeleteImageAsync(oldMainImage), Moq.Times.Once);
    }

    [Fact]
    [Trait("Product", "Handler")]
    public async void UpdateProductHandler_ProductDoesNotExist_ShouldReturnTrue()
    {
        //Arrange
        var productToUpdate = ProductBuilder.BuildProduct();
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        productToUpdate.Id = 3;
        command.SetId(5);
        command.CategoryId= category.Id;
        var handler = CreateUpdateProductHandler(category, productToUpdate);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeFalse();
        response.Errors.Should()
            .ContainSingle(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.PRODUCT_DOES_NOT_EXIST));
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
