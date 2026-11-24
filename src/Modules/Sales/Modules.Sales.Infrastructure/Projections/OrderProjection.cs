using KurrentDB.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Sales.Infrastructure.Context;

namespace Modules.Sales.Infrastructure.Projections;


public class OrderProjection(KurrentDBClient client, IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly KurrentDBClient _client = client;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var start = await LoadCheckpoint(stoppingToken);
        await using var subscriber = _client.SubscribeToStream("$ce-order", start, resolveLinkTos: true, cancellationToken: stoppingToken);
    }
    private async Task<FromStream> LoadCheckpoint(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var salesDbContext = scope.ServiceProvider.GetRequiredService<SalesDbContext>();
        var checkpoint = await salesDbContext.ProjectionCheckpoints.FindAsync(["order-projection"], cancellationToken);
        return checkpoint is null ? FromStream.Start : FromStream.After(StreamPosition.FromInt64(checkpoint.Position));
    }
}