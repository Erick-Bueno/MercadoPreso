using Common.Domain;
using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Products;

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
            return new ValueCannotBeNegative();
        }

        return new Price(value);
    }
}
