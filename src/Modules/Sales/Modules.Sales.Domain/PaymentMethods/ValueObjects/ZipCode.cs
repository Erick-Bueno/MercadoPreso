using Common.Domain;

namespace Modules.Sales.Domain.PaymentMethods.ValueObjects;

public sealed record ZipCode
{
    public string Value { get; private set; }

    private ZipCode(string value)
    {
        Value = value;
    }

    public static Result<ZipCode> Create()
    {
        
    }
}
