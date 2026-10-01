using Common.Domain;
using Common.Domain.Extensions;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Promotions.Events;

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
    => Result.Create((Title: title, Description: description, Discount: discount, Period: period))
        .Ensure(_ => string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title), PromotionErrors.InvalidTitle)
        .Map(properties => new Promotion(
            properties.Title,
            properties.Description,
            properties.Discount,
            properties.Period,
            true,
            PromotionId.Create(Guid.CreateVersion7())
        ));

    public Result<Unit> Activate()
     => Result.Success
            .Ensure(_ => DateTime.UtcNow > Period.End, PromotionErrors.InvalidPromotion)
            .Tap(_ => Active = true);

    public Result<Unit> Deactivate() =>
    Result.Success
            .Ensure(_ => DateTime.UtcNow > Period.End, PromotionErrors.InvalidPromotion)
            .Tap(_ => Active = false)
            .Tap(_ => Raise(new DeactivatePromotionEvent(Id)));
    


}
public sealed record PromotionId
{
    public Guid Value { get; private set; }
    private PromotionId(Guid value)
    {
        Value = value;
    }
    public static PromotionId Create(Guid Value) => new(Value);
}
