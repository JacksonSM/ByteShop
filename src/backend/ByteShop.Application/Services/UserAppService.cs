using ByteShop.Application.Services.Contracts;
using ByteShop.Application.User.RegisterUser;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Application.Services;
public class UserAppService : IUserAppService
{
    private readonly IMediator _mediator;

    public UserAppService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ValidationResult> RegisterCustomer(RegisterCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
}
