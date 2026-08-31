using Common.Domain;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; } = string.Empty;
    public Price Price { get; private set; }
    public Guid? PromotionId { get; private set; }

    private Product(ProductId id, string name, Price price) : base(id)
    {
        Name = name;
        Price = price;
    }

    public static DomainResult<Product> Create(string name, Price price)
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