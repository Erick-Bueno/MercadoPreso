namespace Common.Application;


public interface IEventStoreRepository<T, TId>
{
    public Task Save(T order, CancellationToken cancellationToken);
}