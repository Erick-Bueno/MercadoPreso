using Common.Domain;
namespace Modules.Catalog.Domain.Products.Events;

public record ProductCreatedDomainEvent(ProductId Id) : DomainEvent();