using Common.Domain;
using Common.Domain.Extensions;
using Modules.Sales.Domain.Quotes.Errors;

namespace Modules.Sales.Domain.Quotes;

public sealed class OrderItem : Entity<OrderItemId>
{
    public Guid ProductId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    private OrderItem(OrderItemId id, Guid productId, decimal quantity) : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public static Result<OrderItem> Create(Guid productId, decimal quantity)
        => Result.Create((Quantity: quantity, ProductId: productId))
            .Ensure(_ => quantity >= 0, OrderItemErrors.QuantityShouldNotBeNegative)
            .Map(properties => new OrderItem(OrderItemId.New(), properties.ProductId, properties.Quantity));
}



public readonly record struct OrderItemId(Guid Value)
{
    public static OrderItemId New() => new(Guid.CreateVersion7());
}  