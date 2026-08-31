using System.Collections.ObjectModel;
using Common.Domain;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Products;

namespace Modules.Catalog.Domain.Promotions;

public class Promotion : AggregateRoot<PromotionId>
{
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public Discount Discount { get; private set; }
    public Period Period { get; private set; }
    public bool Active { get; private set; }

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

    public static DomainResult<Promotion> Create(
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
        var promotion = new Promotion(
            title,
            description,
            discount,
            period,
            true,
            PromotionId.Create()
        );

        return promotion;
    }
}

public record PromotionId(Guid Value)
{
    public static PromotionId Create() => new(Guid.CreateVersion7());
}
