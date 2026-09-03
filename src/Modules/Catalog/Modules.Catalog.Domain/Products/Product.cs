using Common.Domain;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; } = string.Empty;
    public Money Price { get; private set; }
    public PromotionId? PromotionId { get; private set; }

    private Product(ProductId id, string name, Money price) : base(id)
    {
        Name = name;
        Price = price;
    }

    public static DomainResult<Product> Create(string name, Money price)
    {

        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return ProductErrors.ProductNameIsRequired;
        }
        return new Product(ProductId.Create(),name, price);
    }
}


public record ProductId(Guid Value)
{
    public static ProductId Create() => new(Guid.CreateVersion7());
};