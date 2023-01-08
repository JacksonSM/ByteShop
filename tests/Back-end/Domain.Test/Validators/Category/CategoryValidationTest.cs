using ByteShop.Domain.Entities;
using ByteShop.Domain.Entities.Validations;
using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Commands;
using Utilities.Entities;
using Xunit;

namespace Validators.Test.Category;
public class CategoryValidationTest
{
    [Fact]
    public void Sucesso()
    {
        var validator = new CategoryValidation();
        var category = CategoryBuilder.BuildCategoryWithTwoLevels();

        var result = validator.Validate(category);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void ValidarErroNomeVazio()
    {
        var validator = new CategoryValidation();
        var category = new ByteShop.Domain.Entities.Category(string.Empty);    

        var result = validator.Validate(category);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_NAME_EMPTY));
    }
    [Fact]
    public void ValidarErroNomeMaiorQue50Caracteres()
    {
        var validator = new CategoryValidation();
        var category = new ByteShop.Domain.Entities.Category(string.Join("", Enumerable.Repeat("x", 62)));

        var result = validator.Validate(category);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.CATEGORY_NAME_MAXIMUMLENGTH));
    }
}
