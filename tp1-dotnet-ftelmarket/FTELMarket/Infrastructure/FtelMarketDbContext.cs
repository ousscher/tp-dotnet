using FTELMarket.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTELMarket.Infrastructure;

public class FtelMarketDbContext : DbContext
{
    // Déclaration des tables de la BDD
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public FtelMarketDbContext(DbContextOptions<FtelMarketDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FtelMarketDbContext).Assembly);
    }
}
