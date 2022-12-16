using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Xunit;

namespace Validators.Test.Products;
public class UpdateProductValidationTest
{
    [Fact]
    public void Sucesso()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ValidarErroNomeVazio()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Name = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_NAME_EMPTY));
    }
    [Fact]
    public void ValidarErroNomeMaiorQue60Caracteres()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Name = string.Join("", Enumerable.Repeat("x", 66));

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_NAME_MAXIMUMLENGTH));
    }

    [Fact]
    public void ValidarErroMarcaVazio()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Brand = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_BRAND_EMPTY));
    }
    [Fact]
    public void ValidarErroMarcaMaiorQue30Caracteres()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Brand = string.Join("", Enumerable.Repeat("x", 34));

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_BRAND_MAXIMUMLENGTH));
    }

    [Fact]
    public void ValidarErroDescricaoVazio()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Description = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_DESCRIPTION_EMPTY));
    }
    [Fact]
    public void ValidarErroSKUVazio()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.SKU = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_SKU_EMPTY));
    }
    [Fact]
    public void ValidarErroPesoMenorOuIgualZero()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Weight = -2f;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_WEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroAlturaMenorOuIgualZero()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Height = -2f;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_HEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroLarguraMenorOuIgualZero()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Width = 0;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_WIDTH_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroComprimentoMenorOuIgualZero()
    {
        var validator = new UpdateProductValidation();
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Length = -2.0f;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_LENGTH_LESS_OR_EQUAL_TO_ZERO));
    }
}
