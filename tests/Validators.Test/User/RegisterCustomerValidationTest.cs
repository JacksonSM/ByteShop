using ByteShop.Application.UseCases.Validations.User;
using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Xunit;

namespace Validators.Test.User;
public class RegisterCustomerValidationTest
{
    [Fact]
    public void Sucesso()
    {
        var validator = new RegisterCustomerValidation();
        var command = UserCommandBuilder.BuildRegisterUserCommand();

        var result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void TestarErroUserNameVazio()
    {
        var validator = new RegisterCustomerValidation();
        var command = UserCommandBuilder.BuildRegisterUserCommand();
        command.UserName = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_USERNAME));
    }

    [Fact]
    public void TestarErroEmailVazio()
    {
        var validator = new RegisterCustomerValidation();
        var command = UserCommandBuilder.BuildRegisterUserCommand();
        command.Email = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_EMAIL));
    }

    [Fact]
    public void TestarErroEmailInvalido()
    {
        var validator = new RegisterCustomerValidation();
        var command = UserCommandBuilder.BuildRegisterUserCommand();
        command.Email = "userbyteshop.com";

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));
    }

    [Fact]
    public void TestarErroCPFVazio()
    {
        var validator = new RegisterCustomerValidation();
        var command = UserCommandBuilder.BuildRegisterUserCommand();
        command.Cpf = string.Empty;

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EMPTY_CPF));
    }

    [Fact]
    public void TestarErroCPFCom5Digitos()
    {
        var validator = new RegisterCustomerValidation();
        var command = UserCommandBuilder.BuildRegisterUserCommand();
        command.Cpf = "12345";

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_CPF));
    }
    [Fact]
    public void TestarErroCPFInvalido()
    {
        var validator = new RegisterCustomerValidation();
        var command = UserCommandBuilder.BuildRegisterUserCommand();
        command.Cpf = "44826037008";

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.INVALID_CPF));
    }
}
