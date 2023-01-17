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
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CostPrice)
               .IsRequired()
                .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Stock)
               .IsRequired();

        builder.Property(x => x.Warranty)
               .IsRequired();

        builder.Property(x => x.Weight)
               .IsRequired()
               .HasColumnType("float(10,3)");

        builder.Property(x => x.Height)
               .IsRequired()
               .HasColumnType("float(10,2)");

        builder.Property(x => x.Length)
               .IsRequired()
                .HasColumnType("float(10,2)");

        builder.Property(x => x.Width)
               .IsRequired()
               .HasColumnType("float(10,2)");

        builder.HasOne(r => r.Category)
               .WithMany(r => r.Products)
               .HasForeignKey(f => f.CategoryId);

        builder.Property("secondaryImageUrl")
               .HasColumnName("SecondaryImageUrl");

        builder.Ignore(c => c.ValidationResult);
        builder.Ignore(c => c.SecondaryImageUrl);
    }
}
