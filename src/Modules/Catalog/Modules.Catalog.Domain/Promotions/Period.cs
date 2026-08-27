using Common.Domain;
using Common.Domain.Errors;

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
            return new EndDateCannotBeBeforeStartDate();
        }  

        return new Period(start, end);
    }
}
