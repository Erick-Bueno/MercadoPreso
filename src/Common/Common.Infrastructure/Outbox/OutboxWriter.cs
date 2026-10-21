using System.Text.Json;
using Common.Application.Interfaces;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using TransactionOutbox = Common.Infrastructure.Outbox.Outbox;


namespace Common.Infrastructure.Outbox;

public class OutboxWriter<TDbContext>(TDbContext context) : IOutboxWriter where TDbContext : DbContext
{
    public Task Write(IIntegrationEvent integrationEvent, string entityType, object aggregateId, CancellationToken cancellationToken)
    {
        context.Set<TransactionOutbox>().Add(new TransactionOutbox($"{entityType}:{aggregateId}",
                    integrationEvent.GetType().FullName!,
                    entityType,
                    JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType())));
        return Task.CompletedTask;
    }
}