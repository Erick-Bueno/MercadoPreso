using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Application.Orders.Interfaces;


public interface IOrderRepository
{
    public void Save(Order order);
}