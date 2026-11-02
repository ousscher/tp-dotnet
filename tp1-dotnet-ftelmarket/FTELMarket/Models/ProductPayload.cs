using System.ComponentModel.DataAnnotations;

namespace FTELMarket.Models;

/// <summary>
/// Corps des requêtes POST /products et PUT /products/{id} (schéma <c>zProductPayload</c>).
/// Les types valeur sont nullables pour qu'un champ absent soit signalé
/// comme manquant au lieu de valoir silencieusement 0.
/// </summary>
public class ProductPayload
{
    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères.")]
    public string? Name { get; set; }

    [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Le prix est obligatoire.")]
    [Range(0.01, 1_000_000, ErrorMessage = "Le prix doit être compris entre 0,01 et 1 000 000.")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "Le stock est obligatoire.")]
    [Range(0, int.MaxValue, ErrorMessage = "Le stock ne peut pas être négatif.")]
    public int? Stock { get; set; }

    [Required(ErrorMessage = "La catégorie est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "La catégorie est obligatoire.")]
    public int? CategoryId { get; set; }
}
