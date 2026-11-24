using Modules.Sales.Domain.Orders.Enums;
using Modules.Sales.Domain.PaymentMethods.Enum;

namespace Modules.Sales.Infrastructure.ReadModels;

public sealed class OrderReadModel
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
}
