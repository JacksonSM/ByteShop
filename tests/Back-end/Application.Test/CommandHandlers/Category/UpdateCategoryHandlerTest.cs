using ByteShop.Application.Category.UpdateCategory;
using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Entities;
using Utilities.Repositories;
using Xunit;

namespace Application.Test.CommandHandlers.Category;

public class UpdateCategoryHandlerTest
{
    [Fact]
    [Trait("Category", "Handler")]
    public async void UpdateCategoryHandler_WithValidData_ShouldReturnTrue()
    {
        //Arrange
        var parentCategory = CategoryBuilder.BuildCategoryWithoutLevel();
        var category = CategoryBuilder.BuildCategoryWithoutLevel(parentCategory);
        var command = new UpdateCategoryCommand { Name = "Processador", ParentCategoryId = parentCategory.ParentCategoryId.Value };
        command.SetId(category.Id);
        var handler = CreateUpdateCategoryHandler(category, parentCategory);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Handler")]
    public async void UpdateCategoryHandler_UpdateWithIdThatDoesNotExist_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var category = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = new UpdateCategoryCommand { Name = "Placa Mãe", ParentCategoryId = 67 };
        command.SetId(category.Id + 23);
        var handler = CreateUpdateCategoryHandler(category);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeFalse();
        response.Errors.Should().ContainSingle(error => error.ErrorMessage
            .Equals(ResourceValidationErrorMessage.CATEGORY_DOES_NOT_EXIST));
    }

    [Fact]
    [Trait("Category", "Handler")]
    public async void UpdateCategoryHandler_UpdateWithParentCategoryThatDoesNotExist_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var category = new ByteShop.Domain.Entities.Category("Coller");
        category.Id = 65;
        var command = new UpdateCategoryCommand { Name = "Cooler", ParentCategoryId = 69 };
        command.SetId(65);

        var handler = CreateUpdateCategoryHandler(category);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeFalse();
        response.Errors.Should().ContainSingle(error => error.ErrorMessage
            .Equals(ResourceValidationErrorMessage.PARENT_CATEGORY_DOES_NOT_EXIST));
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
