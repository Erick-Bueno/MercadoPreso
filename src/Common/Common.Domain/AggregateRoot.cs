namespace Common.Domain;

public class AggregateRoot<TId>(TId id) :
Entity<TId>(id),
IHasDomainEvent where TId : notnull

{
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
}