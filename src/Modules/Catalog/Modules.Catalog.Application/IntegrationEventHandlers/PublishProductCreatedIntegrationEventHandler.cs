using Common.Application.Interfaces;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Domain.Products.Events;
using Modules.Catalog.IntegrationEvents.Products;

namespace Modules.Catalog.Application.IntegrationEventHandlers;


public class PublishProductCreatedIntegrationEventHandler([FromKeyedServices("Catalog")] IOutboxWriter outboxWriter) 
: INotificationHandler<ProductCreatedDomainEvent>
{
    private readonly IOutboxWriter _outboxWriter = outboxWriter;
    public async ValueTask Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var productCreatedIntegrationEvent = new ProductCreatedIntegrationEvent(notification.Id.Value);
        await _outboxWriter.Write(productCreatedIntegrationEvent, "Product", notification.Id, cancellationToken);
    }
}