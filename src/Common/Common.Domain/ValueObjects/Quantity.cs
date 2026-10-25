namespace Common.Domain.ValueObjects;

public sealed record Quantity
{
    private readonly decimal Value { get; private set; }

    private Quantity(decimal value)
    {
        Value = value;
    }

    public static Result<Quantity> Create(decimal value)
        => Result.Create((Quantity: value))
            .

    
}