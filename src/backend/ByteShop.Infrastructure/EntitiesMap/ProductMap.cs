using ByteShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteShop.Infrastructure.EntitiesMap;
public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .HasColumnType("nvarchar(60)")
               .IsRequired();

        builder.Property(x => x.Description)
               .IsRequired();

        builder.Property(x => x.Brand)
               .HasColumnType("nvarchar(30)")
               .IsRequired();

        builder.Property(x => x.SKU)
               .IsRequired();

        builder.Property(x => x.Price)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.CostPrice)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.Stock)
               .IsRequired();

        builder.Property(x => x.Warranty)
               .IsRequired();

        builder.Property(x => x.Weight)
               .HasPrecision(10, 3)
               .IsRequired();

        builder.Property(x => x.Height)
               .HasPrecision(10, 2)
               .IsRequired();

        builder.Property(x => x.Length)
               .HasPrecision(10, 2)
               .IsRequired();

        builder.Property(x => x.Width)
               .HasPrecision(10, 2)
               .IsRequired();

        builder.HasOne(r => r.Category)
               .WithMany(r => r.Products)
               .HasForeignKey(f => f.CategoryId);
    }
}
