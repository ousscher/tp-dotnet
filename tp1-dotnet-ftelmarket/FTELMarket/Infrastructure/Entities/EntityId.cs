namespace FTELMarket.Infrastructure.Entities;

/// <summary>
/// Base commune à toutes les tables : une clé primaire entière auto-incrémentée.
/// </summary>
public abstract class EntityId
{
    public int Id { get; set; }
}
