using System.Collections.ObjectModel;
using Common.Domain;
using Common.Domain.Errors;
using Modules.Catalog.Domain.Enums;
using Modules.Catalog.Domain.Products;

namespace Modules.Catalog.Domain.Promotions;

public class Promotion : Entity
{
    private const int MAX_PRODUCTS_IN_PROMOTION = 1000;

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public decimal DiscountValue { get; private set; }
    public Period Period { get; private set; }
    public bool Active { get; private set; }
    private Collection<Product> _products { get; set; }

    private Promotion(
        string title,
        string? description,
        DiscountType discountType,
        decimal discountValue,
        Period period,
        bool active
    )
    {
        Title = title;
        Description = description;
        DiscountType = discountType;
        DiscountValue = discountValue;
        Period = period;
        Active = active;
        _products = [];
    } 

    public static DomainResult<Promotion> Create(
        string title,
        string? description,
        DiscountType discountType,
        decimal discountValue,
        Period period
    )
    {
        return new Promotion(title, description, discountType, discountValue, period, true);
    }

    public DomainResult<Product> AddProduct(Product product)
    {
        if(_products.Count > MAX_PRODUCTS_IN_PROMOTION)
        {
            return new ProductLimitReachedForThisPromotion();
        }
        if (_products.Contains(product))
        {
            return 
        }
        _products.Add(product);
        return product;
    }
}
