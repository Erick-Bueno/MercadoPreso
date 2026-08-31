using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Infrastructure.Context.Configurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(60);
        builder.Property(p => p.Description)
        .HasColumnType("text");
        builder.Property(p => p.Active).IsRequired();

        builder.OwnsOne(p => p.Period);
        builder.OwnsOne(p => p.Discount);
    }
}
