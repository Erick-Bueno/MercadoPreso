using Common.Domain.Extensions;

namespace Common.Domain.ValueObjects;

public sealed record Quantity
{
    public int Value { get; private set; }

    private Quantity(int value)
    {
        Value = value;
    }

    public static Result<Quantity> Create(int value) =>
        Result
            .Success.Ensure(_ => value >= 0, Errors.DomainError.QuantityCannotBeNegative)
            .Map(_ => new Quantity(value));
}
