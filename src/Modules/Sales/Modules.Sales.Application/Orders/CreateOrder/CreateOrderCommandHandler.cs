using Common.Application;
using Common.Application.Interfaces;
using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler(IEventSourcingRepository<Order, OrderId> eventSourcingRepository) : ICommandHandler<CreateOrderCommand, Unit>
{
    private readonly IEventStoreRepository _orderRepository = orderRepository;

    public async Task<Result<Unit>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var priece = Price.Create(command.TotalPrice);
        var PaymentMethod = PaymentMethod
        var order = Order.Create();

        await _orderRepository.Save();
    }
}