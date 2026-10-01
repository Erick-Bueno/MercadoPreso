using Common.Infrastructure.TransactionOutbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Infrastructure.Context.Configurations;

public class OutboxConfiguration : IEntityTypeConfiguration<Outbox>
{
    public void Configure(EntityTypeBuilder<Outbox> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Payload).HasColumnType("jsonb");
    }
}