using Common.Infrastructure.Messaging.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Modules.Catalog.Infrastructure.Products.Messaging;

public class PromotionDeactivatedConsumer : BackgroundService
{

    private readonly ConsumerOptions _options;
    private readonly ConsumerConfig _consumerConfig;
    public PromotionDeactivatedConsumer(IOptionsMonitor<ConsumerOptions> monitor)
    {
        _options = monitor.Get(ConsumerOptions.Catalog);
        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            GroupId = _options.GroupId,
            AutoOffsetReset = _options.AutoOffsetReset
        };
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    => Task.Run(async () =>
    {
        using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
        consumer.Subscribe(_options.Topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);
                //logica + inbox
                consumer.Commit(result);
            }
        }
        catch (OperationCanceledException) { }
        finally { consumer.Close(); }
    }, stoppingToken);
}