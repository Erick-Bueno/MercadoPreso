using Common.Domain;
using Common.Domain.Extensions;
using Modules.Sales.Domain.Orders.Enums;
using Modules.Sales.Domain.Orders.Events;
using Modules.Sales.Domain.PaymentMethods;

namespace Modules.Sales.Domain.Orders;

public class Order : EventSourcedAggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public Price TotalPrice { get; private set; } = null!;
    public PaymentMethod PaymentMethod { get; private set; } = null!;
    public OrderStatus Status { get; private set; }

    private Order(OrderId id)
        : base(id) { }

    public static Result<Order> Create(Price totalPrice, PaymentMethod paymentMethod, List<OrderItem> items)
        => Result.Success.Map(_ =>
        {
            var order = new Order(OrderId.New());
            order.Raise(new OrderCreatedDomainEvent(totalPrice, paymentMethod, items));
            return order;
        });



    protected override void Apply(DomainEvent @event)
    {
        switch (@event)
        {
            case OrderCreatedDomainEvent orderCreated:
                Status = OrderStatus.Pending;
                PaymentMethod = orderCreated.PaymentMethod;
                _items.AddRange(orderCreated.Items.Select(item => OrderItem.Create(item.ProductId, item.Quantity, item.UnitPrice).Value));
                TotalPrice = orderCreated.TotalPrice;
                break;
        }
    }

}

public readonly record struct OrderId(Guid Value)
{
    public static OrderId New() => new(Guid.CreateVersion7());
}
