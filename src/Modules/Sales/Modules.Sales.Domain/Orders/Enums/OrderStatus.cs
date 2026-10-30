namespace Modules.Sales.Domain.Orders.Enums;

public enum OrderStatus
{
    PendingPayment,
    Paid,
    Shipped,
    Delivered,
    Canceled,
    Refunded,
}
