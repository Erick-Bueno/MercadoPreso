using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Sales.Domain.Orders.Enums;
using Modules.Sales.Domain.PaymentMethods;

namespace Modules.Sales.Domain.Orders.Events;


public sealed record OrderCreatedDomainEvent(OrderId Id, Price TotalPrice, PaymentMethod PaymentMethod, List<OrderItem> Items) : DomainEvent;