using ByteShop.Application.Category.AddCategory;
using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Entities;
using Utilities.Repositories;
using Xunit;

namespace Application.Test.CommandHandlers.Category;
public class AddCategoryHandlerTest
{
    [Fact]
    [Trait("Category", "Handler")]
    public async void AddCategoryHandler_WithValidData_ShouldReturnTrue()
    {
        //Arrange
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        command.ParentCategoryId = 0;
        var handler = CreateAddCategoryHandler();

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Handler")]
    public async void AddCategoryHandler_CreatingSecondaryCategory_ShouldReturnTrue()
    {
        //Arrange
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        command.ParentCategoryId = categoryParent.Id;
        var handler = CreateAddCategoryHandler(categoryParent);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Handler")]
    public async void AddCategoryHandler_CreatingWithParentCategoryThatDoesNotExist_ShouldReturnFalseWithErrorMessage()
    {
        //Arrange
        var categoryParent = CategoryBuilder.BuildCategoryWithoutLevel();
        var command = CategoryCommandBuilder.AddCategoryCommandBuild();
        categoryParent.Id = 45;
        command.ParentCategoryId = 5;
        var handler = CreateAddCategoryHandler(categoryParent);

        //Act
        var response = await handler.Handle(command, CancellationToken.None);

        //Assert
        response.IsValid.Should().BeFalse();
        response.Errors.Should().ContainSingle(error => error.ErrorMessage
            .Equals(ResourceValidationErrorMessage.PARENT_CATEGORY_DOES_NOT_EXIST));
    }

    private static AddCategoryHandler CreateAddCategoryHandler(ByteShop.Domain.Entities.Category category = null)
    {
        var categoryRepo = CategoryRepositoryBuilder
            .Instance()
            .SetupGetByIdWithAssociationAsync(category)
            .Build();
        var uow = UnitOfWorkBuilder.Instance().Build();

        return new AddCategoryHandler(categoryRepo, uow);
    }
}
