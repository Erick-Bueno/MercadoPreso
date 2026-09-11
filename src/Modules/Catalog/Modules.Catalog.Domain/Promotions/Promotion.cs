using Common.Domain;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Promotions;

public class Promotion : AggregateRoot<PromotionId>
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public Discount Discount { get; private set; } = null!;
    public Period Period { get; private set; } = null!;
    public bool Active { get; private set; }

    private Promotion()
        : base(default!) { }

    private Promotion(
        string title,
        string? description,
        Discount discount,
        Period period,
        bool active,
        PromotionId id
    )
        : base(id)
    {
        Title = title;
        Description = description;
        Discount = discount;
        Period = period;
        Active = active;
    }

    public static Result<Promotion> Create(
        string title,
        string? description,
        Discount discount,
        Period period
    )
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
        {
            return PromotionErrors.InvalidTitle;
        }
        return new Promotion(
            title,
            description,
            discount,
            period,
            true,
            PromotionId.Create(Guid.CreateVersion7())
        );

    }
}
public record PromotionId
{
    public Guid Value { get; private set; }
    private PromotionId(Guid value)
    {
        Value = value;
    }
    public static PromotionId Create(Guid Value) => new(Value);
}
