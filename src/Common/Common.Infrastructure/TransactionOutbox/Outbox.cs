using System.Reflection.Metadata;
using Common.Domain;

namespace Common.Infrastructure.TransactionOutbox;


public sealed class Outbox : Entity<OutboxId>
{
    public string Type { get; private set; }
    public string Payload { get; private set; }
    public DateTime OcurredOnUtc { get; private set; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    private Outbox(string type, string payload, OutboxId id) : base(id)
    {
        Type = type;
        Payload = payload;
        OcurredOnUtc = DateTime.UtcNow;
    }

    public static Outbox Create(string type, string payload) => new(type, payload, OutboxId.Create(Guid.CreateVersion7()));
}


public sealed record OutboxId
{
    public Guid Value { get; private set; }

    private OutboxId(Guid value)
    {
        Value = value;
    }
    public static OutboxId Create(Guid value) => new(value);
}