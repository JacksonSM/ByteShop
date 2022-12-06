using ByteShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ByteShop.Infrastructure.Context;
public class ByteShopDbContext : DbContext
{
    public ByteShopDbContext(DbContextOptions<ByteShopDbContext> options) : base(options){}

    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ByteShopDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
