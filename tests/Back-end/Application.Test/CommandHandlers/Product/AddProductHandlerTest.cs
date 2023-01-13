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
    [Trait("Product", "Handler")]
    public async void AddProductHandler_WithValidData_ShouldReturnTrue()
    {
        //Arrange
        var command = ProductCommandBuilder.AddProductCommandBuild();
        var handler = CreateAddProductHandler(command.CategoryId);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.ValidationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "Handler")]
    public async void AddProductHandler_CategoryDoesNotExist_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.CategoryId = 0;
        var handler = CreateAddProductHandler(command.CategoryId);
        CancellationTokenSource cts = new CancellationTokenSource();

        //Act
        var response = await handler.Handle(command, cts.Token);

        //Assert
        response.ValidationResult.IsValid.Should().BeFalse();
        response.ValidationResult.Errors.Should()
                    .ContainSingle(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST));
    }

    [Fact]
    [Trait("Product", "Handler")]
    public async void UploadBase64ImageAsync_WithValidData_ShouldReturnTrueAndTheServiceWillReceiveTheCorrectValues()
    {
        //Arrange
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

        //Act
        var response = await handler.Handle(command, CancellationToken.None);


        //Assert
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
    [Trait("Product", "Handler")]
    public async void AddProductHandler_CommandNoImage_ShouldReturnTrue()
    {
        //Arrange
        var command = ProductCommandBuilder.AddProductCommandBuild(numberSecondaryImages: 0);
        command.MainImageBase64 = null;
        var handler = CreateAddProductHandler(command.CategoryId);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.ValidationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Product", "Handler")]
    public async void AddProductHandler_AddProductWithMainImageOnly_ShouldReturnTrue()
    {
        //Arrange
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

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
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
