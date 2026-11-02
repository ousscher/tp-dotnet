namespace FTELMarket.Infrastructure.Entities;

/// <summary>
/// Catégorie regroupant plusieurs produits.
/// </summary>
public class Category : EntityId
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
