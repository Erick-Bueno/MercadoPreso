using Common.Domain;
using Common.Domain.Extensions;
using Modules.Catalog.Domain.Errors;

namespace Modules.Catalog.Domain.Promotions;

public sealed record Period
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static Result<Period> Create(DateTime start, DateTime end) =>
        Result.Create((Start: start, End: end)) 
            .Ensure(properties => properties.End < properties.Start, PromotionErrors.EndDateCannotBeBeforeStartDate)
            .Map(properties => new Period(properties.Start, properties.End));
}