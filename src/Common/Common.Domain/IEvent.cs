namespace Common.Domain;

public interface IEvent
{
    public void Raise(DomainEvent domainEvent);
    public void ClearEvents();
    public IReadOnlyList<DomainEvent> GetDomainEvents();
}