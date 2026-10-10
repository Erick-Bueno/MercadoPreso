using Common.Infrastructure.TransactionOutbox;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Context.Converters;

public class OutboxIdConverter() : ValueConverter<OutboxId, Guid>(id => id.Value, value => OutboxId.Create(value));