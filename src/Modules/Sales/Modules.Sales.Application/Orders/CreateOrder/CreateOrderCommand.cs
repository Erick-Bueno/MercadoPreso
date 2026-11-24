using Common.Application.Interfaces;
using Common.Domain;
using Modules.Sales.Domain.PaymentMethods.Enum;

namespace Modules.Sales.Application.Orders.CreateOrder;


public record InstallmentRequest(
    int Count,
    int IntervalInDays
);
public record OrderItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);

public record PaymentMethodRequest(
    PaymentType PaymentType,
    InstallmentRequest? Installment,
    string Description
);

public record CreateOrderCommand(
    List<OrderItemRequest> Items,
    decimal TotalPrice,
    PaymentMethodRequest PaymentMethod

) : ICommand<Unit>;