using ByteShop.Application.Commands.User;
using ByteShop.Application.Services.Contracts;
using ByteShop.Infra.CrossCutting.Bus;
using FluentValidation.Results;

namespace ByteShop.Application.Services;
public class UserAppService : IUserAppService
{
    private readonly IMediatorHandler _mediator;

    public UserAppService(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    public async Task<ValidationResult> RegisterCustomer(RegisterCustomerCommand command)
    {
        var result = await _mediator.SendCommand(command);
        return result;
    }
}
