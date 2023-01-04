using ByteShop.Application.Commands.User;
using FluentValidation.Results;

namespace ByteShop.Application.Services.Contracts;
public interface IUserAppService
{
    Task<ValidationResult> RegisterCustomer(RegisterCustomerCommand command);
}
