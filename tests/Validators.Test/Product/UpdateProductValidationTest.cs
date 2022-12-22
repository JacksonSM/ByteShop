using ByteShop.Application.UseCases.Validations.Product;
using ByteShop.Exceptions;
using FluentAssertions;
using FluentValidation;
using Utilities.Commands;
using Utilities.Entities;
using Xunit;

namespace Validators.Test.Products;
public class UpdateProductValidationTest
{
    [Fact]
    public void Sucesso()
    {
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
        var command = ProductCommandBuilder.UpdateProductCommandBuild();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ValidarErroNomeVazio()
    {
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
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
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.Length = -2.0f;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PRODUCT_LENGTH_LESS_OR_EQUAL_TO_ZERO));
    }
    [Fact]
    public void ProdutoComImagemMaiorQue350KB()
    {
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
        var command = ProductCommandBuilder.UpdateProductCommandBuild(withImageOfMoreThan350KB: true);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.MAX_IMAGE_SIZE));
    }
    [Fact]
    public void ProdutoComMaisDe5Imagens()
    {
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToAdd: 7,
            numberOfSecondaryImagesToRemove: 1);
        var validator = new UpdateProductValidation(produtcToUpdate);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.MAXIMUM_AMOUNT_OF_IMAGES));
    }
    [Fact]
    public void ProdutoSemImagemPrincipal()
    {
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
        var command = ProductCommandBuilder.UpdateProductCommandBuild(changeMainImage: false);
        produtcToUpdate.SetMainImage("");

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.MUST_HAVE_A_MAIN_IMAGE));
    }
    [Fact]
    public void ProdutoSemImagens()
    {
        var produtcToUpdate = ProductBuilder.ProductBuild();
        var validator = new UpdateProductValidation(produtcToUpdate);
        var command = ProductCommandBuilder.UpdateProductCommandBuild(
            numberOfSecondaryImagesToRemove: 0,
            numberOfSecondaryImagesToAdd: 0,
            changeMainImage: false);

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }
}
