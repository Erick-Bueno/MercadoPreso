using Common.Domain;
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
        builder.Property(p => p.Description).HasColumnType("text");
        builder.Property(p => p.Active).IsRequired();

        builder.ComplexProperty(
            p => p.Period,
            period =>
            {
                period.Property(x => x.Start).HasColumnName("period_start");
                period.Property(x => x.End).HasColumnName("period_end");
            }
        );

        builder.ComplexProperty(
            p => p.Discount,
            discount =>
            {
                discount.Property(x => x.DiscountType).HasColumnName("discount_type");
                discount
                    .Property(x => x.Price)
                    .HasColumnName("discount_price")
                    .HasColumnType("numeric(18,2)")
                    .HasConversion(price => price.Value, value => Price.Create(value).Value);
            }
        );
    }
}
