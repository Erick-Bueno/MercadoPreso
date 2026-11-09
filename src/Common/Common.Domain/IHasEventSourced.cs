namespace Common.Domain;

public interface IHasEventSourced
{
    public void Raise(DomainEvent @event);
    public IReadOnlyCollection<DomainEvent> GetUncommitedEvents();
    public int Version { get; set; }
}