using Common.Domain;
using Common.Domain.Extensions;
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
    public static Result<Discount> Create(DiscountType discountType, Price price) =>
        Result.Create((DiscountdiscountType: discountType, Price: price))
            .Ensure(properties => discountType == DiscountType.Percentage && (properties.Price.Value > MAX_PERCENTAGE), PromotionErrors.InvalidPercentage)
            .Map(properties => new Discount(properties.DiscountdiscountType, properties.Price));


}
