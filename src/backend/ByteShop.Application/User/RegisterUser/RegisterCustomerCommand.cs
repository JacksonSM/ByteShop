using ByteShop.Application.Configuration.Command;
using FluentValidation.Results;

namespace ByteShop.Application.User.RegisterUser;
public class RegisterCustomerCommand : CommandBase<ValidationResult>
{
    public string UserName { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
