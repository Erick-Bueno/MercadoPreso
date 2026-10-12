using Confluent.Kafka;

namespace Common.Infrastructure.Messaging.Options;

public class ConsumerOptions
{
    public const string SectionName = "Kafka";
    public const string Catalog = "Catalog";
    public string BootstrapServers { get; init; } = string.Empty;
    public string GroupId { get; init; } = string.Empty;
    public string Topic { get; init; } = string.Empty;
    public AutoOffsetReset AutoOffsetReset { get; init; } = AutoOffsetReset.Earliest;
}