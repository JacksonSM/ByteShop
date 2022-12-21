using FluentAssertions;
using ByteShop.Application.UseCases.Handlers.Product;
using Utilities.Commands;
using Utilities.Entities;
using Utilities.Mapper;
using Utilities.Repositories;
using Utilities.Services;
using Xunit;
using ByteShop.Exceptions.Exceptions;
using ByteShop.Exceptions;

namespace Handlers.Test.Product;
public class UpdateProductHandlerTest
{
    [Fact]
    public async void Sucesso()
    {
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        var productToUpdate = ProductBuilder.ProductBuild();
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
        command.CategoryId = 0;
        var handler = CreateUpdateProductHandler(command.CategoryId, productToUpdate);

        Func<Task> action = async () => { await handler.Handle(command); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 &&
            exception.ErrorMessages.Contains(ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));
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
