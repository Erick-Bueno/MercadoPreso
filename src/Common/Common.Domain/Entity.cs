namespace Common.Domain;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }

    public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
}
