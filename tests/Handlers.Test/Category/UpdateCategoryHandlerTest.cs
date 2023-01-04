using ByteShop.Application.CommandHandlers.Category;
using ByteShop.Application.Commands.Category;
using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Entities;
using Utilities.Repositories;
using Xunit;

namespace Handlers.Test.Category;

public class UpdateCategoryHandlerTest
{
    [Fact]
    public async void Sucesso()
    {
        var parentCategory = CategoryBuilder.BuildCategoryWithoutLevel();
        var category = CategoryBuilder.BuildCategoryWithoutLevel(parentCategory);

        var command = new UpdateCategoryCommand { Name = "Processador", ParentCategoryId = parentCategory.ParentCategoryId.Value };
        command.SetId(category.Id);

        var handler = CreateUpdateCategoryHandler(category, parentCategory);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);

        response.IsValid.Should().BeTrue();
    }

    [Fact]
    public async void AtualizarComIdInexistente()
    {
        var category = CategoryBuilder.BuildCategoryWithoutLevel();

        var command = new UpdateCategoryCommand { Name = "Placa Mãe", ParentCategoryId = 67 };
        command.SetId(category.Id + 23);

        var handler = CreateUpdateCategoryHandler(category);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token);


        response.IsValid.Should().BeFalse();
        response.Errors.Any(error => error.ErrorMessage.Equals(ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST))
            .Should().BeTrue();
    }

    [Fact]
    public async void AtualizarComCategoriaPaiInexistente()
    {
        var category = new ByteShop.Domain.Entities.Category("Coller");
        category.Id = 65;
        var command = new UpdateCategoryCommand { Name = "Cooler", ParentCategoryId = 69 };
        command.SetId(65);

        var handler = CreateUpdateCategoryHandler(category);

        CancellationTokenSource cts = new CancellationTokenSource();
        var response = await handler.Handle(command, cts.Token); 

        response.IsValid.Should().BeFalse();
        response.Errors.Any(error => error.ErrorMessage.Equals(ResourceErrorMessages.PARENT_CATEGORY_DOES_NOT_EXIST))
            .Should().BeTrue();
    }

    private static UpdateCategoryHandler CreateUpdateCategoryHandler(
        ByteShop.Domain.Entities.Category category = null,
        ByteShop.Domain.Entities.Category categoryParent = null)
    {
        var categoryRepo = CategoryRepositoryBuilder
            .Instance()
            .SetupGetById(categoryParent)
            .SetupGetByIdWithAssociationAsync(category)
            .Build();
        var uow = UnitOfWorkBuilder.Instance().Build();

        return new UpdateCategoryHandler(categoryRepo, uow);
    }
}
