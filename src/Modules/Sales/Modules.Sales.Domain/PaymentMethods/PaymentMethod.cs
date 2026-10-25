using Modules.Sales.Domain.PaymentMethods.Enum;

namespace Modules.Sales.Domain.PaymentMethods;

public class PaymentMethod
{
    public PaymentType PaymentType;
}

public readonly record struct PaymentMethodId(Guid Value)
{
    public static PaymentMethodId New() => new(Guid.CreateVersion7());
}