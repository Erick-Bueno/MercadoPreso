namespace Common.Domain;

public class EventSourcedAggregateRoot<TId>(TId id) :
Entity<TId>(id), 
IHasEventSourced
where TId : notnull
{
    private readonly List<DomainEvent> _uncommitedEvents = [];

    public IReadOnlyCollection<DomainEvent> GetUncommitedEvents() => _uncommitedEvents.AsReadOnly();

    public void Raise(DomainEvent @event)
    {
        _uncommitedEvents.Add(@event);
        Apply(@event);

    }
    protected virtual void Apply(DomainEvent @event){}
}