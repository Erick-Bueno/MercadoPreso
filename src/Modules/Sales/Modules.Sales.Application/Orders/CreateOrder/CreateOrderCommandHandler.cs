using Common.Application.Interfaces;
using Common.Domain;
using Modules.Sales.Application.Orders.Interfaces;
using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository) : ICommandHandler<CreateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public Task<Result<Unit>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create();
    }
}