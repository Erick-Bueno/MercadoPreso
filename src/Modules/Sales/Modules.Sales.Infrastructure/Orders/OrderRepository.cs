using KurrentDB.Client;
using Modules.Sales.Application.Orders.Interfaces;
using Modules.Sales.Domain.Orders;

namespace Modules.Sales.Infrastructure.Orders;

public class OrderRepository(KurrentDBClient client) : IOrderRepository
{
    private readonly KurrentDBClient _client = client;

    public void Save(Order order)
    {
        var orderEvents = order.GetUncommitedEvents().ToList();   
        orderEvents.Select(orderEvent => new EventData(
            
        ));
    }
}