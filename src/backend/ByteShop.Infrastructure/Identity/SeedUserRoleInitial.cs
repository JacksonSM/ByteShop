using ByteShop.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace ByteShop.Infrastructure.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager,
          UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "Customer@localhost";
            user.Email = "Customer@localhost";
            user.NormalizedUserName = "CUSTOMER@LOCALHOST";
            user.NormalizedEmail = "CUSTOMER@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(user, "Customer@123");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "admin@localhost";
            user.Email = "admin@localhost";
            user.NormalizedUserName = "ADMIN@LOCALHOST";
            user.NormalizedEmail = "ADMIN@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(user, "Admin@123");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }

    }

    public async Task SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("Customer").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Customer";
            role.NormalizedName = "CUSTOMER";
            await _roleManager.CreateAsync(role);
        }
        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            await _roleManager.CreateAsync(role);
        }
    }
}
