using Common.Domain;
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

    public static DomainResult<Period> Create(DateTime start, DateTime end)
    {
       if(end < start)
        {
            return PromotionErrors.EndDateCannotBeBeforeStartDate;
        }  

        return new Period(start, end);
    }
}