namespace FTELMarket.Infrastructure.Entities;

/// <summary>
/// Utilisateur de la place de marché : il possède un catalogue de produits.
/// </summary>
public class User : EntityId
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
