using ByteShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteShop.Infrastructure.EntitiesMap;
public class CategoryMap
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .HasColumnType("nvarchar(50)")
               .IsRequired();

        builder.HasOne(r => r.ParentCategory)
               .WithMany(r => r.ChildCategories)
               .HasForeignKey(f => f.ParentCategoryId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.NoAction);


    }
}
