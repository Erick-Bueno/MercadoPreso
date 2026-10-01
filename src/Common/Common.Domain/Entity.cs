namespace Common.Domain;

public class Entity<TId>(TId id) : IEvent
{
    public TId Id { get; private set; } = id;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }
    private readonly List<DomainEvent> _domainEvents = [];
    public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
    public void Raise(DomainEvent domainEvent){
        _domainEvents.Add(domainEvent);
    }
    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
    public IReadOnlyList<DomainEvent> GetDomainEvents() => [.. _domainEvents];
}

