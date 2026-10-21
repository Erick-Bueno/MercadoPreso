using Common.IntegrationEvents;

namespace Modules.Catalog.IntegrationEvents.Products;

public sealed record ProductCreatedIntegrationEvent(
    Guid ProductId) : IIntegrationEvent;