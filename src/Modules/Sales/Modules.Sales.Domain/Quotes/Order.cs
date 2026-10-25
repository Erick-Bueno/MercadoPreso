using Common.Domain;

namespace Modules.Sales.Domain.Quotes;


public class Quote : AggregateRoot<QuoteId>
{
    private readonly List<OrderItem> _items = [];
    private Quote(QuoteId id) : base(id)
    {
        
    }
}


public readonly record struct QuoteId(Guid Value)
{
    public static QuoteId New() => new(Guid.CreateVersion7());
}