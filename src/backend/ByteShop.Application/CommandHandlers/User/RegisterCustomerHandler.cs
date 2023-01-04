using ByteShop.Application.Commands.User;
using ByteShop.Application.Commands.Validations.User;
using ByteShop.Domain.Account;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ByteShop.Application.CommandHandlers.User;
public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, ValidationResult>
{
    private const string USER_ROLE = "CUSTOMER";

    private readonly IAccountService _account;

    public RegisterCustomerHandler(
        IAccountService account)
    {
        _account = account;
    }

    public async Task<ValidationResult> Handle(RegisterCustomerCommand command, CancellationToken cancellation)
    {
        var validator = new RegisterCustomerValidation();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid) return validationResult;

        (bool sucess, string[] errors) = await _account.RegisterUser
            (
                email: command.Email,
                password: command.Password,
                cpf: command.Cpf,
                role: USER_ROLE
            );

        var validationFailures = ConvertFailures(errors);
        validationResult.Errors.AddRange(validationFailures);

        if (sucess)
        {
            return validationResult;
        }
        else
        {
            return validationResult;
        }
    }

    private ValidationFailure[] ConvertFailures(string[] errors)
    {
        var listErrors = new List<ValidationFailure>();
        foreach (var error in errors)
        {
            listErrors.Add(new ValidationFailure(string.Empty, error));
        }
        return listErrors.ToArray();
    }
}
