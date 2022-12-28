using Microsoft.AspNetCore.Identity;

namespace ByteShop.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string CPF { get; set; }
}
