using ByteShop.Application.Category.AddCategory;
using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Entities;
using Utilities.Mapper;
using Utilities.Repositories;
using Utilities.Services;
using Xunit;

namespace Handlers.Test.Category;
public class AddCategoryHandlerTest
{
    [Fact]
    public async void CriandoCategoriaPrincipal()
    {
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        command.ParentCategoryId = 0;

        var handler = CreateAddCategoryHandler();

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);
        
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async void CriandoCategoriaSegundaria()
    {
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        command.ParentCategoryId = categoryParent.Id;

        var handler = CreateAddCategoryHandler(categoryParent);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async void CriandoComACategoriaPaiInexistente()
    {
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        categoryParent.Id = 45;
        command.ParentCategoryId = 5;

        var handler = CreateAddCategoryHandler(categoryParent);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeFalse();
        response.Errors.Any(error => error.ErrorMessage.Equals(ResourceErrorMessages.PARENT_CATEGORY_DOES_NOT_EXIST))
            .Should().BeTrue();
    }

    private static AddCategoryHandler CreateAddCategoryHandler(ByteShop.Domain.Entities.Category category = null)
    {
        var categoryRepo = CategoryRepositoryBuilder
            .Instance()
            .SetupGetByIdWithAssociationAsync(category)
            .Build();
        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<AddCategoryHandler>.Instance().Build();

        return new AddCategoryHandler(categoryRepo, uow);
    }
}
