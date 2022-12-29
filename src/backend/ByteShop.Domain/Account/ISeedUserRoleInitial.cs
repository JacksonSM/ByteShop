namespace ByteShop.Domain.Account;

public interface ISeedUserRoleInitial
{
    Task SeedUsers();
    Task SeedRoles();
}