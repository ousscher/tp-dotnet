using FTELMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTELMarket.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(entity => entity.Name).IsUnique();

        // Une catégorie contenant des produits ne peut pas être supprimée.
        builder.HasMany(e => e.Products)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Category { Id = 1, Name = "PlayStation 5" },
            new Category { Id = 2, Name = "Nintendo Switch" },
            new Category { Id = 3, Name = "Xbox Series X" },
            new Category { Id = 4, Name = "PC" });
    }
}
