using Common.Application;
using Common.Application.Interfaces;
using Common.Domain;
using Common.Domain.Errors;
using Common.Domain.Extensions;
using Common.Domain.ValueObjects;
using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler(IEventStoreRepository<Order, OrderId> eventSourcingRepository) : ICommandHandler<CreateOrderCommand, Unit>
{
    private readonly IEventStoreRepository<Order, OrderId> _eventSourcingRepository = eventSourcingRepository;

    public async Task<Result<Unit>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        switch (command.ToOrder())
        {
            case DomainError error:
                return error;
            case Order order:
                await _eventSourcingRepository.Save(order, cancellationToken);
                return Unit.Value;
            default:
                throw new InvalidOperationException();
        }
    }
}