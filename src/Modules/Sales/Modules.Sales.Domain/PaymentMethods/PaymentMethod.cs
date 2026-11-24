using System.Collections.ObjectModel;
using Common.Domain;
using Common.Domain.Extensions;
using Modules.Sales.Domain.PaymentMethods.Enum;
using Modules.Sales.Domain.PaymentMethods.ValueObjects;

namespace Modules.Sales.Domain.PaymentMethods;

public class PaymentMethod : AggregateRoot<PaymentMethodId>
{
    public PaymentType PaymentType { get; private set; }
    public Installment? Installment { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    private PaymentMethod(
        PaymentMethodId id,
        string description,
        PaymentType paymentType,
        Installment? installment
    )
        : base(id)
    {
        Description = description;
        PaymentType = paymentType;
        Installment = installment;
    }

    public static Result<PaymentMethod> Create(
        string description,
        PaymentType paymentType,
        Installment? installment
    ) =>
        Result
            .Create(
                (Description: description, PaymentType: paymentType, Installment: installment)
            )
            .Map(properties => new PaymentMethod(
                PaymentMethodId.New(),
                properties.Description,
                properties.PaymentType,
                properties.Installment
            ));
}

public readonly record struct PaymentMethodId(Guid Value)
{
    public static PaymentMethodId New() => new(Guid.CreateVersion7());
}
