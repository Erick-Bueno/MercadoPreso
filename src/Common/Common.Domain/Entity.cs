namespace Common.Domain;

public abstract class Entity<TId>(TId id)
{
    public TId Id { get; private set; } = id;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }

    public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
}
