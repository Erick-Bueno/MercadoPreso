using Common.Domain;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Domain.Products.Events;

public record ProductCreatedEvent(ProductId Id) : DomainEvent();