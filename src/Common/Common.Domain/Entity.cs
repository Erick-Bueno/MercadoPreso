using System.Runtime.CompilerServices;

namespace Common.Domain;

public class Entity<TId>(TId id)
    where TId : notnull
{
    public TId Id { get; private set; } = id;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
