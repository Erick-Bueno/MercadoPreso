using Common.Domain;
using Modules.Catalog.Domain.Enums;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Promotions;

public record Discount
{
    private const int MAX_PERCENTAGE = 100;
    public DiscountType DiscountType { get; private set; }
    public Price Price { get; private set; }
    private Discount(DiscountType discountType, Price price)
    {
        DiscountType = discountType;
        Price = price;
    }
    public static DomainResult<Discount> Create(DiscountType discountType, Price price)
    {
        if (discountType == DiscountType.Percentage && (price.Value > MAX_PERCENTAGE))
        {
            return PromotionErrors.InvalidPercentage;
        }

        return new Discount(discountType, price);
    }
}
