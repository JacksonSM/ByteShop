namespace ByteShop.Application.UseCases.Commands.User;
public class RegisterCustomerCommand : ICommand
{
    public string UserName { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
