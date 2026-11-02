using FTELMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTELMarket.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(entity => entity.Description)
            .HasMaxLength(1000);

        // decimal(18,2) : prix exact au centime près.
        builder
            .Property(entity => entity.Price)
            .HasPrecision(18, 2);

        builder
            .Property(entity => entity.CreatedAt)
            .IsRequired();

        builder.ToTable(table =>
        {
            table.HasCheckConstraint("CK_Product_Price", "[Price] > 0");
            table.HasCheckConstraint("CK_Product_Stock", "[Stock] >= 0");
        });

        // Jeu de données initial, identique aux données mockées du front.
        builder.HasData(
            Seed(1, "The Last of Us Part II", "Boîte et notice, disque sans rayure.", 19.90m, 3, 1, "2026-09-02T09:15:00Z"),
            Seed(2, "Gran Turismo 7", null, 29.90m, 1, 1, "2026-09-05T14:30:00Z"),
            Seed(3, "The Legend of Zelda: Tears of the Kingdom", "Cartouche seule.", 39.90m, 2, 2, "2026-09-08T10:00:00Z"),
            Seed(4, "Mario Kart 8 Deluxe", "Complet, très bon état.", 34.90m, 5, 2, "2026-09-10T16:45:00Z"),
            Seed(5, "Halo Infinite", null, 14.90m, 4, 3, "2026-09-12T11:20:00Z"),
            Seed(6, "Forza Horizon 5", "Édition standard.", 24.90m, 0, 3, "2026-09-15T08:05:00Z"),
            Seed(7, "Baldur’s Gate 3", "Clé physique, coffret collector.", 44.90m, 1, 4, "2026-09-18T17:40:00Z"),
            Seed(8, "Hades", null, 9.90m, 6, 4, "2026-09-21T13:10:00Z"));
    }

    private static Product Seed(int id, string name, string? description, decimal price, int stock, int categoryId, string createdAt) =>
        new()
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            Stock = stock,
            CategoryId = categoryId,
            UserId = UserConfiguration.DefaultUserId,
            CreatedAt = DateTimeOffset.Parse(createdAt, System.Globalization.CultureInfo.InvariantCulture),
        };
}
