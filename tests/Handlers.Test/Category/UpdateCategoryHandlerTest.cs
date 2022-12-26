using ByteShop.Application.UseCases.Commands.Category;
using ByteShop.Application.UseCases.Handlers.Category;
using ByteShop.Exceptions.Exceptions;
using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Mapper;
using Utilities.Repositories;
using Xunit;
using Utilities.Services;

namespace Handlers.Test.Category;

public class UpdateCategoryHandlerTest
{
    [Fact]
    public async void Sucesso()
    {
        var category = new ByteShop.Domain.Entities.Category("CPU");
        category.Id = 36;
        var command = new UpdateCategoryCommand { Name = "Processador", ParentCategoryId = 23 };
        command.SetId(36);

        var handler = CreateUpdateCategoryHandler(category, command.ParentCategoryId);

        var response = await handler.Handle(command);

        response.StatusCode.Should().Be(200);

        response.Data.Name.Should().Be(command.Name);
        response.Data.ParentCategoryId.Should().Be(command.ParentCategoryId);
    }

    [Fact]
    public async void AtualizarComIdInexistente()
    {
        var category = new ByteShop.Domain.Entities.Category("Placa mae");
        category.Id = 36;
        var command = new UpdateCategoryCommand { Name = "Placa Mãe", ParentCategoryId = 67 };
        command.SetId(65);

        var handler = CreateUpdateCategoryHandler(category, command.ParentCategoryId);

        Func<Task> action = async () => { await handler.Handle(command); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 &&
            exception.ErrorMessages.Contains(ResourceErrorMessages.CATEGORY_DOES_NOT_EXIST));
    }

    [Fact]
    public async void AtualizarComCategoriaPaiInexistente()
    {
        var category = new ByteShop.Domain.Entities.Category("Coller");
        category.Id = 65;
        var command = new UpdateCategoryCommand { Name = "Cooler", ParentCategoryId = 69 };
        command.SetId(65);

        var handler = CreateUpdateCategoryHandler(category);

        Func<Task> action = async () => { await handler.Handle(command); };

        await action.Should().ThrowAsync<ValidationErrorsException>()
            .Where(exception => exception.ErrorMessages.Count == 1 &&
            exception.ErrorMessages.Contains(ResourceErrorMessages.PARENT_CATEGORY_DOES_NOT_EXIST));
    }

    private static UpdateCategoryHandler CreateUpdateCategoryHandler(
        ByteShop.Domain.Entities.Category category = null,
        int categoryParentId = 0)
    {
        var categoryRepo = CategoryRepositoryBuilder
            .Instance()
            .SetupExistById(categoryParentId)
            .SetupGetByIdAsync(category)
            .Build();
        var mapper = MapperBuilder.Instance();
        var uow = UnitOfWorkBuilder.Instance().Build();
        var logger = LoggerBuilder<AddCategoryHandler>.Instance().Build();

        return new UpdateCategoryHandler(categoryRepo, uow, mapper,logger);
    }
}
