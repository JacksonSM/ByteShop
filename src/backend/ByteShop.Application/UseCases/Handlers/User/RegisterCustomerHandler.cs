using ByteShop.Application.UseCases.Commands.User;
using ByteShop.Application.UseCases.Results;
using ByteShop.Application.UseCases.Validations.User;
using ByteShop.Domain.Account;
using ByteShop.Exceptions.Exceptions;
using Microsoft.Extensions.Logging;

namespace ByteShop.Application.UseCases.Handlers.User;
public class RegisterCustomerHandler : IHandler<RegisterCustomerCommand, object>
{
    private const string USER_ROLE = "CUSTOMER";

    private readonly IAccountService _account;
    private readonly ILogger<RegisterCustomerHandler> _logger;

    public RegisterCustomerHandler(
        IAccountService account,
        ILogger<RegisterCustomerHandler> logger)
    {
        _account = account;
        _logger = logger;
    }

    public async Task<RequestResult<object>> Handle(RegisterCustomerCommand command)
    {
        _logger.LogInformation("Entered the register customer handler");

        Validate(command);
        (bool sucess, string[] errors) = await _account.RegisterUser
            (
                email: command.Email,
                password: command.Password,
                cpf: command.Cpf,
                role: USER_ROLE
            );

        if (sucess)
        {
            _logger.LogInformation("Account created successfully");
            return new RequestResult<object>().Created(null);
        }
        else
        {
            _logger.LogInformation("there was a failure creating the account: {@errors}", errors);
            _logger.LogDebug("Command: {@command}", command);
            return new RequestResult<object>().BadRequest("Não foi possivel registrar sua conta.", errors);
        }
    }

    private void Validate(RegisterCustomerCommand command)
    {
        var validator = new RegisterCustomerValidation();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            _logger.LogInformation("A validation error occurred: {@errorMessages}", errorMessages);
            _logger.LogDebug("Command: {@command}", command);
            throw new ValidationErrorsException(errorMessages);
        }
    }
}
