using Bogus;
using ByteShop.Application.UseCases.Handlers.Category;
using ByteShop.Domain.Entities;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Exceptions;
using ByteShop.Exceptions.Exceptions;
using FluentAssertions;
using Moq;
using System;
using Utilities.Commands;
using Utilities.Entities;
using Utilities.Mapper;
using Utilities.Repositories;
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

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(201);
        response.Data.Name.Should().Be(command.Name);
        response.Data.ParentCategoryId.Should().BeNull();
    }

    [Fact]
    public async void CriandoCategoriaSegundaria()
    {
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        command.ParentCategoryId = categoryParent.Id;

        var handler = CreateAddCategoryHandler(categoryParent);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(201);
        response.Data.Name.Should().Be(command.Name);
        response.Data.ParentCategoryId.Should().Be(command.ParentCategoryId);
    }

    [Fact]
    public async void CriandoComACategoriaPaiInexistente()
    {
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        categoryParent.Id = 45;
        command.ParentCategoryId = 5;

        var handler = CreateAddCategoryHandler(categoryParent);

        Func<Task> action = async () => { await handler.Handle(command); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 &&
            exception.ErrorMessages.Contains(ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));
    }

    private static AddCategoryHandler CreateAddCategoryHandler(ByteShop.Domain.Entities.Category category = null)
    {
        var categoryRepo = CategoryRepositoryBuilder
            .Instance()
            .SetupGetByIdWithAssociationAsync(category)
            .Build();
        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();

        return new AddCategoryHandler(categoryRepo, uow, mapper);
    }
}
