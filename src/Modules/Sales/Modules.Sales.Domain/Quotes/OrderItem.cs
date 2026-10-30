using Common.Domain;
using Common.Domain.Extensions;
using Common.Domain.ValueObjects;

namespace Modules.Sales.Domain.Quotes;

public sealed class OrderItem : Entity<OrderItemId>
{
    public Guid ProductId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Price UnitPrice { get; private set; }

    private OrderItem(OrderItemId id, Guid productId, Quantity quantity, Price unitPrice)
        : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static Result<OrderItem> Create(Guid productId, Quantity quantity, Price unitPrice) =>
        Result
            .Create((Quantity: quantity, ProductId: productId, UnitPrice: unitPrice))
            .Map(properties => new OrderItem(
                OrderItemId.New(),
                properties.ProductId,
                properties.Quantity,
                properties.UnitPrice
            ));
}

public readonly record struct OrderItemId(Guid Value)
{
    public static OrderItemId New() => new(Guid.CreateVersion7());
}
