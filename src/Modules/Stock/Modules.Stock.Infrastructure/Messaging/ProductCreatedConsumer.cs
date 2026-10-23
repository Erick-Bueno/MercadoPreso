using Common.Infrastructure.Messaging.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Modules.Stock.Infrastructure.Messaging;

public class ProductCreatedConsumer : BackgroundService
{

    private readonly ConsumerOptions _options;
    private readonly ConsumerConfig _consumerConfig;
    public ProductCreatedConsumer(IOptionsMonitor<ConsumerOptions> monitor)
    {
        _options = monitor.Get(ConsumerOptions.Stock);
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