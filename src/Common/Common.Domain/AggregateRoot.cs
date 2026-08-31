namespace Common.Domain;

public class AggregateRoot<TId>(TId id) : Entity<TId>(id)
{
}