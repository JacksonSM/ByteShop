using ByteShop.Domain.Entities;
using ByteShop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ByteShop.Infrastructure.Context;
public class ByteShopDbContext : IdentityDbContext<ApplicationUser>
{
    public ByteShopDbContext(DbContextOptions<ByteShopDbContext> options) : base(options) { }

    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ByteShopDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
