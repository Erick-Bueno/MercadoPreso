namespace Common.Domain;

public class EventSourcedAggregateRoot<TId>(TId id) :
Entity<TId>(id),
IHasEventSourced
where TId : notnull
{
    private readonly List<DomainEvent> _uncommitedEvents = [];

    public int Version { get; set; } = -1;

    public IReadOnlyCollection<DomainEvent> GetUncommitedEvents() => _uncommitedEvents.AsReadOnly();

    public void Raise(DomainEvent @event)
    {
        _uncommitedEvents.Add(@event);
        Apply(@event);

    }

    public void LoadFromHistory(DomainEvent @event, int eventNumber)
    {
        Apply(@event);
        Version = eventNumber;
    }
    protected virtual void Apply(DomainEvent @event) { }
}