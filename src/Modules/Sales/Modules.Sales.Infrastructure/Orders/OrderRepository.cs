using System.Text.Json;
using System.Text.Json.Nodes;
using KurrentDB.Client;
using Modules.Sales.Application.Orders.Interfaces;
using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Infrastructure.Orders;

public class OrderRepository(KurrentDBClient client) : IOrderRepository
{
    private readonly KurrentDBClient _client = client;

    public async Task Save(Order order, CancellationToken cancellationToken)
    {
        var orderEvents = order.GetUncommitedEvents().ToList();
        var eventsData = orderEvents.Select(orderEvent => new EventData(
            Uuid.FromGuid(Guid.CreateVersion7()),
            orderEvent.GetType().Name,
            JsonSerializer.SerializeToUtf8Bytes(orderEvent, orderEvent.GetType())
        )).ToArray();

        StreamState expected = order.Version == -1
      ? StreamState.NoStream
      : StreamState.StreamRevision((ulong)order.Version);

        await _client.AppendToStreamAsync($"order-{order.Id}", expected, eventsData, cancellationToken: cancellationToken);
    }
}