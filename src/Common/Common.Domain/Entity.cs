using System.Runtime.CompilerServices;

namespace Common.Domain;

public class Entity<TId>(TId id) : IEvent, IEntity
    where TId : notnull
{
    public TId Id { get; private set; } = id;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    private readonly List<DomainEvent> _domainEvents = [];
    
    public void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    public IReadOnlyList<DomainEvent> GetDomainEvents() => [.. _domainEvents];

    public object GetId() => Id;
}
