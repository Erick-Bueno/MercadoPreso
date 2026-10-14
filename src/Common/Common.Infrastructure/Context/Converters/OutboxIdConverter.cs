using Common.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Context.Converters;

public class OutboxIdConverter() : ValueConverter<OutboxId, Guid>(id => id.Value, value => OutboxId.Create(value));