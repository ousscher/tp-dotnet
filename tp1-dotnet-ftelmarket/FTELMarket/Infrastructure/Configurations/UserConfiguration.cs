using FTELMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTELMarket.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    // Pas encore d'authentification : tous les produits sont rattachés à cet utilisateur.
    public const int DefaultUserId = 1;

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(e => e.Products)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.HasData(new User { Id = DefaultUserId, Name = "Vendeur démo" });
    }
}
