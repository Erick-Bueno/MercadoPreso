using Common.Application.Interfaces;
using Common.Domain;
using Modules.Sales.Domain.PaymentMethods.Enum;

namespace Modules.Sales.Application.Orders.CreateOrder;


public record InstallmentData(
    int Count,
    decimal Amount,
    decimal TotalAmount
);
public record OrderItemData(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);

public record PaymentMethodData(
    PaymentType PaymentType,
    List<InstallmentData>? Installments,
    string Description
);

public record CreateOrderCommand(
    List<OrderItemData> Items,
    decimal TotalPrice,
    PaymentMethodData PaymentMethod

) : ICommand<Unit>;