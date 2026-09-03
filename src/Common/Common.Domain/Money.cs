using Common.Domain.Errors;

namespace Common.Domain;

public sealed record Money
{
    public decimal Value { get; }

    private Money(decimal value)
    {
        Value = value;
    }
    public static DomainResult<Money> Create(decimal value)
    {
        if(value < 0)
        {
            return DomainError.ValueCannotBeNegative;
        }

        return new Money(value);
    }
}
