using Common.Domain;
using Modules.Catalog.Domain.Promotions;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; } = string.Empty;
    public Price Price { get; private set; }
    public PromotionId? PromotionId { get; private set; }


    private Product(ProductId id, string name, Price price) : base(id)
    {
        Name = name;
        Price = price;
    }

    public static Result<Product> Create(string name, Price price) =>
     Result.Create((Name: name, Price: price))
             .Ensure(
                properties => string.IsNullOrWhiteSpace(properties.Name),
                ProductErrors.ProductNameIsRequired)
             .Map(product => new Product(ProductId.Create(Guid.CreateVersion7()), product.Name, product.Price));


    public Result<Unit> ActivatePromotion(PromotionId promotionId) =>
        Result.Success
        .Ensure(_ => PromotionId != null, PromotionErrors.ProductAlreadyHasAnActivePromotion)
        .Tap(unit => PromotionId = promotionId);

}


public record ProductId
{
    public Guid Value { get; private set; }

    private ProductId(Guid value)
    {
        Value = value;
    }
    public static ProductId Create(Guid value) => new(value);
};