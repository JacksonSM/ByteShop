using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Xunit;

namespace Validators.Test.Product;
public class AddProductValidationTest
{
    [Fact]
    public void Sucesso()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ValidarErroNomeVazio()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Name = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_NAME_EMPTY));
    }
    [Fact]
    public void ValidarErroNomeMaiorQue60Caracteres()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Name = string.Join("", Enumerable.Repeat("x", 66));

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_NAME_MAXIMUMLENGTH));
    }

    [Fact]
    public void ValidarErroMarcaVazio()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Brand = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_BRAND_EMPTY));
    }
    [Fact]
    public void ValidarErroMarcaMaiorQue30Caracteres()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Brand = string.Join("", Enumerable.Repeat("x", 34));

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_BRAND_MAXIMUMLENGTH));
    }

    [Fact]
    public void ValidarErroDescricaoVazio()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Description = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_DESCRIPTION_EMPTY));
    }
    [Fact]
    public void ValidarErroSKUVazio()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.SKU = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_SKU_EMPTY));
    }
    [Fact]
    public void ValidarErroPesoMenorOuIgualZero()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Weight = -2f;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_WEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroAlturaMenorOuIgualZero()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Height = -2f;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_HEIGHT_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroLarguraMenorOuIgualZero()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Width = 0;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_WIDTH_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ValidarErroComprimentoMenorOuIgualZero()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.Length = -2.0f;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_LENGTH_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ProdutoComImagemMaiorQue350KB()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild(withImageOfMoreThan350KB: true);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.MAX_IMAGE_SIZE));
    }
    [Fact]
    public void ProdutoComMaisDe5Imagens()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild(numberSecondaryImages: 7);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.MAXIMUM_AMOUNT_OF_IMAGES));
    }
    [Fact]
    public void ProdutoSemImagemPrincipal()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.MainImageBase64 = null;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.MUST_HAVE_A_MAIN_IMAGE));
    }
    [Fact]
    public void ProdutoSemImagens()
    {
        var validator = new AddProductValidation();
        var command = ProductCommandBuilder.AddProductCommandBuild();
        command.MainImageBase64 = null;
        command.SecondaryImagesBase64 = null;

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }
}
