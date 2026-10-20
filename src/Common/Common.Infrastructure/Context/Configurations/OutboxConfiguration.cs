using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionOutbox = Common.Infrastructure.Outbox.Outbox;
namespace Common.Infrastructure.Context.Configurations;

public class OutboxConfiguration : IEntityTypeConfiguration<TransactionOutbox>
{
    public void Configure(EntityTypeBuilder<TransactionOutbox> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Payload).HasColumnType("jsonb");
    }
}