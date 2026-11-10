using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Application.Orders.Interfaces;


public interface IEventSourcingRepository<T, TId>
{
    public Task Save(T order, CancellationToken cancellationToken);
}