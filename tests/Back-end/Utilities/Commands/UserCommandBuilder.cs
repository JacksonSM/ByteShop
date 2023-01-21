using Bogus;
using ByteShop.Application.User.RegisterUser;

namespace Utilities.Commands;
public class UserCommandBuilder
{
    public static RegisterCustomerCommand BuildRegisterUserCommand()
    {
        var cpfs = new string[] { "49890535084", "25802161035", "44826057008", "38360608091" };
        return new Faker<RegisterCustomerCommand>()
            .RuleFor(c => c.UserName, f => f.Internet.UserName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Password, f => f.Internet.Password())
            .RuleFor(c => c.Cpf, f => f.PickRandom(cpfs));
    }
}
