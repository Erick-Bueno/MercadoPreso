using Common.Domain;
using Modules.Sales.Domain.Orders.Enums;
using Modules.Sales.Domain.PaymentMethods;

namespace Modules.Sales.Domain.Orders.Events;


public sealed record OrderCreatedDomainEvent(Price TotalPrice, PaymentMethod PaymentMethod, List<OrderItem> Items) : DomainEvent;