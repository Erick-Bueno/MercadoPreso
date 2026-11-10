using Common.Application.Interfaces;
using Common.Domain;
using Modules.Sales.Domain.PaymentMethods.Enum;

namespace Modules.Sales.Application.Orders.CreateOrder;


public record InstallmentRequest(
    int Count,
    decimal Amount,
    decimal TotalAmount
);
public record OrderItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);

public record PaymentMethodRequest(
    PaymentType PaymentType,
    List<InstallmentRequest>? Installments,
    string Description
);

public record CreateOrderCommand(
    List<OrderItemRequest> Items,
    decimal TotalPrice,
    PaymentMethodRequest PaymentMethod

) : ICommand<Unit>;