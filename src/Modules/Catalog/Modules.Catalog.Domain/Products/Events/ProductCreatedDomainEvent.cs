using Common.Domain;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Domain.Products.Events;

public record ProductCreatedDomainEvent(ProductId Id) : DomainEvent();