using System.Collections.ObjectModel;
using Common.Domain;
using Modules.Catalog.Domain.Enums;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Products;

namespace Modules.Catalog.Domain.Promotions;

public class Promotion : Entity
{
    private const int MAX_PRODUCTS_IN_PROMOTION = 1000;

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public Price DiscountValue { get; private set; }
    public Period Period { get; private set; }
    public bool Active { get; private set; }
    private Collection<Product> Products { get; set; }

    private Promotion(
        string title,
        string? description,
        DiscountType discountType,
        Price discountValue,
        Period period,
        bool active,
        Collection<Product> products
    )
    {
        Title = title;
        Description = description;
        DiscountType = discountType;
        DiscountValue = discountValue;
        Period = period;
        Active = active;
        Products = products;
    }

    public static DomainResult<Promotion> Create(
        string title,
        string? description,
        DiscountType discountType,
        Price discountValue,
        Period period,
        Collection<Product> products
    )
    {
        if (products.Count > MAX_PRODUCTS_IN_PROMOTION)
        {
            return PromotionErrors.ProductLimitReached;
        }
        return new Promotion(
            title,
            description,
            discountType,
            discountValue,
            period,
            true,
            products
        );
    }

    public DomainResult<Product> AddProduct(Product product)
    {
        if (Products.Contains(product))
        {
            return PromotionErrors.ProductAlreadyHasAnActivePromotion;
        }
        Products.Add(product);
        return product;
    }

    public bool IsActive() => Active;
}
