using System.Collections.ObjectModel;
using Common.Domain;
using Common.Domain.Extensions;
using Modules.Sales.Domain.PaymentMethods.Enum;
using Modules.Sales.Domain.PaymentMethods.ValueObjects;

namespace Modules.Sales.Domain.PaymentMethods;

public class PaymentMethod : AggregateRoot<PaymentMethodId>
{
    public PaymentType PaymentType { get; private set; }
    private readonly List<Installment>? _installments = [];
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    private PaymentMethod(
        PaymentMethodId id,
        string description,
        PaymentType paymentType,
        List<Installment>? installments
    )
        : base(id)
    {
        Description = description;
        PaymentType = paymentType;
        _installments = installments;
    }

    public static Result<PaymentMethod> Create(
        string description,
        PaymentType paymentType,
        List<Installment>? installments
    ) =>
        Result
            .Create(
                (Description: description, PaymentType: paymentType, Installments: installments)
            )
            .Map(properties => new PaymentMethod(
                PaymentMethodId.New(),
                properties.Description,
                properties.PaymentType,
                properties.Installments
            ));
}

public readonly record struct PaymentMethodId(Guid Value)
{
    public static PaymentMethodId New() => new(Guid.CreateVersion7());
}
