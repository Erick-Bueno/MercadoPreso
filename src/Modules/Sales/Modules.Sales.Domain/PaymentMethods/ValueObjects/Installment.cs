using Common.Domain;
using Common.Domain.Errors;
using Common.Domain.Extensions;

namespace Modules.Sales.Domain.PaymentMethods.ValueObjects;

public sealed record Installment
{
    public int Count { get; private set; }
    public int IntervalInDays { get; private set; }

    private Installment(int count, int intervalInDays)
    {
        Count = count;
        IntervalInDays = intervalInDays;
    }

    public static Result<Installment> Create(int count, int intervalInDays) =>
        Result
            .Success.Ensure(_ => count > 0, DomainError.InstallmentCountMustBeGreaterThanZero)
            .Ensure(_ => intervalInDays > 0, DomainError.InstallmentIntervalMustBeGreaterThanZero)
            .Map(_ => new Installment(count, intervalInDays));
}
