
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
        public Result<Order> ToOrder(CreateOrderCommand command)
        {
            var totalPrice = Price.Create(command.TotalPrice);
            if(totalPrice is {IsFailure: true} result)
            {
                return result.Error;
            }
            var installments = Installment
            var paymentMethod = PaymentMethod.Create(command.PaymentMethod.Description, command.PaymentMethod.PaymentType, )
            var order = Order.Create(totalPrice, );
        }
    }
}