using System.Reflection.Metadata;
using System.Text.Json;
using KurrentDB.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Modules.Sales.Domain.Orders.Enums;
using Modules.Sales.Domain.Orders.Events;
using Modules.Sales.Infrastructure.Context;
using Modules.Sales.Infrastructure.ReadModels;

namespace Modules.Sales.Infrastructure.Projections;

public class OrderProjection(KurrentDBClient client, IServiceScopeFactory scopeFactory)
    : BackgroundService
{
    private readonly KurrentDBClient _client = client;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var start = await LoadCheckpoint(stoppingToken);
        await using var subscription = _client.SubscribeToStream(
            "$ce-order",
            start,
            resolveLinkTos: true,
            cancellationToken: stoppingToken
        );
        await foreach (var message in subscription.Messages.WithCancellation(stoppingToken))
        {
            switch (message)
            {
                case StreamMessage.Event(var evnt):
                    await Handle(evnt, stoppingToken);
                    break;
            }
        }
    }

    private async Task Handle(ResolvedEvent evnt, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var salesDbContext = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
       
        if (evnt.IsResolved)
        {
            await Apply(evnt.Event, salesDbContext);
        }

        var currentPosition = evnt.OriginalEventNumber.ToInt64();

        var checkpoint = await salesDbContext.ProjectionCheckpoints.FindAsync(
            ["order-projection"],
            cancellationToken
        );
        if (checkpoint is null)
        {
            return;
        }
        checkpoint.Position = currentPosition;

        await salesDbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task Apply(EventRecord eventRecord, SalesDbContext dbContext)
    {
        switch (eventRecord.EventType)
        {
            case nameof(OrderCreatedDomainEvent):
                var e = JsonSerializer.Deserialize<OrderCreatedDomainEvent>(eventRecord.Data.Span)!;
                dbContext.Orders.Add(
                    new OrderReadModel
                    {
                        Id = e.Id.Value,
                        PaymentType = e.PaymentMethod.PaymentType,
                        Status = OrderStatus.Pending,
                        TotalPrice = e.TotalPrice.Value
                    }
                );
                break;
        }
    }

    private async Task<FromStream> LoadCheckpoint(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var salesDbContext = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
        var checkpoint = await salesDbContext.ProjectionCheckpoints.FindAsync(
            ["order-projection"],
            cancellationToken
        );
        return checkpoint is null
            ? FromStream.Start
            : FromStream.After(StreamPosition.FromInt64(checkpoint.Position));
    }
}
