﻿using ByteShop.Application.User.RegisterUser;
using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Commands;
using Xunit;

namespace Application.Test.Command.User;
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
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.EMPTY_USERNAME));
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
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.EMPTY_EMAIL));
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
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.INVALID_EMAIL));
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
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.EMPTY_CPF));
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
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.INVALID_CPF));
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
            .Contain(error => error.ErrorMessage.Equals(ResourceValidationErrorMessage.INVALID_CPF));
    }
}
