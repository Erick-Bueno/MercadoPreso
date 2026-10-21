using Common.IntegrationEvents;

namespace Common.Application.Interfaces;

public interface IOutboxWriter
{
    public Task Write(IIntegrationEvent integrationEvent, string entityType, object aggregateId, CancellationToken cancellationToken);
}