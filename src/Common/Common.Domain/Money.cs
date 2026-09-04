using Common.Domain.Errors;

namespace Common.Domain;

public sealed record Price
{
    public decimal Value { get; }
    private Price(decimal value)
    {
        Value = value;
    }
    public static DomainResult<Price> Create(decimal value)
    {
        if(value < 0)
        {
            return DomainError.ValueCannotBeNegative;
        }

        return new Price(value);
    }
}
