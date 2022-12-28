namespace ByteShop.Domain.Account;

public interface IAccountService
{
    /// <summary>
    /// Verificar se o usuário está registrado no sistema.
    /// </summary>
    /// <returns>Retorna true se o usuario estiver no sistema, caso contrario, false</returns>
    Task<bool> Authenticate(string email, string password);

    /// <summary>
    /// Registra um novo usuario no sistema.
    /// </summary>
    /// <param name="role">Papel que o usuario terá no sistema</param>
    /// <returns>
    ///Caso o registro do usuario for bem-sucedido o primeiro valor será um true e no segundo valor será null.
    ///Agora no caso de erro no registro, será retornado no primeiro valor false e no segundo as mensagens de erros
    ///</returns>
    Task<(bool ,string[])> RegisterUser(string email, string password, string cpf, string role);
    Task Logout();
}
