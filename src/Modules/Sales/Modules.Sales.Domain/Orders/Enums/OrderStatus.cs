namespace Modules.Sales.Domain.Orders.Enums;

public enum OrderStatus
{
    PendingPayment,
    Pending,
    Paid,
    Shipped,
    Delivered,
    Canceled,
    Refunded,
}
