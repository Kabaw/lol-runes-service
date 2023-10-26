namespace Lol_Runes_Service.Domain.Models;

public abstract class Entity
{
    public long Id { get; protected set; }
    public DateTimeOffset CreateDate { get; protected set; } = DateTimeOffset.Now;
}