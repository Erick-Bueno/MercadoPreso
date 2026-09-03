using Common.Domain;
using Modules.Catalog.Domain.Enums;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Promotions;

public record Discount
{
    private const int MAX_PERCENTAGE = 100;
    public DiscountType DiscountType { get; private set; }
    public Money Money { get; private set; }

    private Discount(DiscountType discountType, Money money)
    {
        DiscountType = discountType;
        Money = money;
    }

    public static DomainResult<Discount> Create(DiscountType discountType, Money money)
    {
        if (discountType == DiscountType.Percentage && (money.Value > MAX_PERCENTAGE))
        {
            return PromotionErrors.InvalidPercentage;
        }

        return new Discount(discountType, money);
    }
}
