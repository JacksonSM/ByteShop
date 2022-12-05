using ByteShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteShop.Infrastructure.Context.EntitiesMap;
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
               .HasPrecision(18,2)
               .IsRequired();
        
        builder.Property(x => x.CostPrice)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.Stock)
               .IsRequired();
        
        builder.Property(x => x.Warranty)
               .IsRequired();
        
        builder.Property(x => x.Weight)
               .IsRequired();
        
        builder.Property(x => x.Heigth)
               .IsRequired();
        
        builder.Property(x => x.Lenght)
               .IsRequired();

        builder.HasOne(r => r.Category)
               .WithMany(r => r.Products)
               .HasForeignKey(f => f.CategoryId);
    }
}
