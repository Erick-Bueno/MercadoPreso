namespace Common.Domain;

public sealed class Outbox : Entity<OutboxId>
{
    public string AggregateId { get; private set; }
    public string AggregateType { get; private set; }
    public string Type { get; private set; }
    public string Payload { get; private set; }
    public DateTime OcurredOnUtc { get; private set; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    private Outbox(string aggregateId, string type, string payload, OutboxId id, string aggregateType)
        : base(id)
    {
        AggregateId = aggregateId;
        AggregateType = aggregateType;
        Type = type;
        Payload = payload;
        OcurredOnUtc = DateTime.UtcNow;
    }

    public static Outbox Create(string type, string payload, string aggregateId, string aggregateType) =>
        new(aggregateId, type, payload, OutboxId.Create(Guid.CreateVersion7()), aggregateType);
}


public readonly record struct OutboxId(Guid Value)
{
        public static OutboxId Create(Guid value) => new(value);

}
