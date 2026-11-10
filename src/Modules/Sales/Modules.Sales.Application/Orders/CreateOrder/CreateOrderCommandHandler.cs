using Common.Application.Interfaces;
using Common.Domain;
using Modules.Sales.Application.Orders.Interfaces;
using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler(IEventSourcingRepository<Order, OrderId> eventSourcingRepository) : ICommandHandler<CreateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Result<Unit>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create();

        await _orderRepository.Save
    }
}