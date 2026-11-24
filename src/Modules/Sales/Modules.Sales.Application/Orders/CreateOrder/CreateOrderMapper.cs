
using Common.Domain;
using Common.Domain.Extensions;
using Common.Domain.ValueObjects;
using Modules.Sales.Domain.Orders;
using Modules.Sales.Domain.PaymentMethods;
using Modules.Sales.Domain.PaymentMethods.ValueObjects;

namespace Modules.Sales.Application.Orders.CreateOrder;

public static class CreateOrderMapper
{
    extension(CreateOrderCommand command)
    {
        public Result<Order> ToOrder()
        {
            var paymentMethod = ToPaymentMethod(command.PaymentMethod);
            var price = Price.Create(command.TotalPrice);
            var items = command.Items.Select(ToOrderItem);
            return Result.Combine(paymentMethod, price, Result.Combine(items))
            .Bind(properties => Order.Create(properties.Item2, properties.Item1, properties.Item3));
        }

        private static Result<PaymentMethod> ToPaymentMethod(PaymentMethodRequest paymentMethod) =>
            paymentMethod.Installment is null
                ? PaymentMethod.Create(paymentMethod.Description, paymentMethod.PaymentType, null)
                : ToInstallment(paymentMethod.Installment)
                    .Bind(installment => PaymentMethod.Create(
                        paymentMethod.Description,
                        paymentMethod.PaymentType,
                        installment
                    ));

        private static Result<Installment> ToInstallment(InstallmentRequest installment) =>
            Installment.Create(installment.Count, installment.IntervalInDays);

        private static Result<OrderItem> ToOrderItem(OrderItemRequest item) =>
        Result.Combine(Price.Create(item.UnitPrice), Quantity.Create(item.Quantity))
        .Bind(properties => OrderItem.Create(item.ProductId, properties.Item2, properties.Item1));

    }
}