namespace Common.Infrastructure.Outbox;

public sealed class Outbox(string aggregateId, string aggregateType, string type, string payload)
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public string AggregateId { get; } = aggregateId;
    public string AggregateType { get; } = aggregateType;
    public string Type { get; } = type;
    public string Payload { get; } = payload;
    public DateTime OcurredOnUtc { get; private set; } = DateTime.UtcNow;
}
