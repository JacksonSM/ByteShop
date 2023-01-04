using ByteShop.Infra.CrossCutting.Bus;

namespace ByteShop.Application.Commands.User;
public class RegisterCustomerCommand : Command
{
    public string UserName { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
