using Common.Domain;
using Common.Domain.Extensions;
using Modules.Stock.Domain.Errors;

namespace Modules.Stock.Domain.StockItems;

public class StockItem : AggregateRoot<StockItemId>
{
    public Guid ProductId { get; private set; }
    public int QuantityAvailable { get; private set; }

    private StockItem(StockItemId id, Guid productId, int quantityAvailable)
        : base(id)
    {
        ProductId = productId;
        QuantityAvailable = quantityAvailable;
    }

    public static Result<StockItem> Create(Guid productId, int quantityAvailable) =>
        Result
            .Create((ProductId: productId, QuantityAvailable: quantityAvailable))
            .Ensure(
                properties => properties.QuantityAvailable >= 0,
                StockItemErrors.AvailableStockCannotBeNegative
            )
            .Map(stockItem => new StockItem(
                StockItemId.New(),
                stockItem.ProductId,
                stockItem.QuantityAvailable
            ));
}

public readonly record struct StockItemId(Guid Value)
{
    public static StockItemId New() => new(Guid.CreateVersion7());
}
