namespace Common.Domain;

public interface IHasDomainEvent
{
    public void Raise(DomainEvent domainEvent);
    public void ClearEvents();
    public IReadOnlyList<DomainEvent> GetDomainEvents();
}