using Common.Domain;
using Modules.Sales.Domain.Orders.Enums;
using Modules.Sales.Domain.PaymentMethods;
using Modules.Sales.Domain.Quotes;

namespace Modules.Sales.Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = [];
    public Price TotalPrice { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public OrderStatus Status { get; private set; }

    private Order(OrderId id)
        : base(id) { }
}

public readonly record struct OrderId(Guid Value)
{
    public static OrderId New() => new(Guid.CreateVersion7());
}
