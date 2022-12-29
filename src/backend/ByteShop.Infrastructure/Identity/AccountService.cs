using ByteShop.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ByteShop.Infrastructure.Identity;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email,
            password, false, lockoutOnFailure: false);

        return result.Succeeded;
    }
    public async Task<(bool, string[])> RegisterUser(string email, string password, string cpf, string role)
    {
        var user = new ApplicationUser
        {
            Email = email,
            CPF = cpf,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
            return (result.Succeeded, null);
        }
        else
        {
            var errorsMessages = result.Errors.Select(x => x.Description).ToArray();
            return (result.Succeeded, errorsMessages);
        }
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
