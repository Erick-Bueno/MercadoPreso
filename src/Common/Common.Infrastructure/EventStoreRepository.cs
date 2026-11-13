
using System.Text.Json;
using Common.Application;
using Common.Domain;
using KurrentDB.Client;

namespace Common.Infrastructure;

public class EventStoreRepository<T, TId>(KurrentDBClient client) :
    IEventStoreRepository<T, TId>
    where T : EventSourcedAggregateRoot<TId> 
    where TId : notnull
{
    private readonly KurrentDBClient _client = client;

    public async Task Save(T entity, CancellationToken cancellationToken)
    {
        var entityEvents = entity.GetUncommitedEvents().ToList();
        var eventsData = entityEvents
            .Select(entityEvent => new EventData(
                Uuid.FromGuid(Guid.CreateVersion7()),
                entityEvent.GetType().Name,
                JsonSerializer.SerializeToUtf8Bytes(entityEvent, entityEvent.GetType())
            ))
            .ToArray();

        StreamState expected =
            entity.Version == -1
                ? StreamState.NoStream
                : StreamState.StreamRevision((ulong)entity.Version);

        await _client.AppendToStreamAsync(
            $"{typeof(T).Name}-{entity.Id}",
            expected,
            eventsData,
            cancellationToken: cancellationToken
        );
        entity.MarkAsCommited();
    }
}
