namespace FTELMarket.Infrastructure.Entities;

/// <summary>
/// Produit du catalogue d'un utilisateur, rangé dans une et une seule catégorie.
/// </summary>
public class Product : EntityId
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // decimal : représentation exacte en base 10, pas d'erreur d'arrondi
    // binaire comme avec float/double (0.1 + 0.2 != 0.3), indispensable pour de l'argent.
    public decimal Price { get; set; }

    public int Stock { get; set; }

    // DateTimeOffset : l'instant est stocké avec son décalage, et l'API le
    // renvoie toujours en UTC. Chaque client le convertit dans son fuseau.
    public DateTimeOffset CreatedAt { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
