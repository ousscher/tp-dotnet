namespace FTELMarket.Models;

/// <summary>
/// Produit tel qu'attendu par le front (schéma <c>zProduct</c>).
/// </summary>
public class ProductResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    // Dénormalisé : le front affiche la catégorie sans second appel.
    public string CategoryName { get; set; } = string.Empty;

    // DateTime de Kind UTC : sérialisé au format ISO 8601 avec le suffixe "Z".
    public DateTime CreatedAt { get; set; }
}
