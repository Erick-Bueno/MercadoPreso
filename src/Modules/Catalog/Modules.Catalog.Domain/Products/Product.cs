using Common.Domain;
using Modules.Catalog.Domain.Promotions;
using Modules.Catalog.Domain.Errors;
using Common.Domain.Extensions;

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
                properties => !string.IsNullOrWhiteSpace(properties.Name),
                ProductErrors.ProductNameIsRequired)
             .Map(product => new Product(ProductId.New(), product.Name, product.Price));


    public Result<Unit> AssignPromotion(Promotion newPromotion) =>
        Result.Success
        .Ensure(_ => PromotionId == null, ProductErrors.ProductAlreadyHasAnActivePromotion)
        .Ensure(_ => newPromotion.Active, PromotionErrors.InvalidPromotion)
        .Tap(_ => PromotionId = newPromotion.Id);

    public Result<Unit> RemovePromotion() => 
    Result.Success
        .Ensure(_ => PromotionId != null, ProductErrors.ProductDoesNotHavePromotion)
        .Tap(_ => PromotionId = null);

}


public readonly record struct ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.CreateVersion7());
}