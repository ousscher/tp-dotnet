namespace FTELMarket.Models;

/// <summary>
/// Catégorie telle qu'attendue par le front (schéma <c>zCategory</c>).
/// </summary>
public class CategoryResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
