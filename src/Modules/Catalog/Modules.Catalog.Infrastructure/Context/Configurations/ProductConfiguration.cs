using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Infrastructure.Context.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(60);
        builder.OwnsOne(p => p.Price);

        builder
            .HasOne<Promotion>()
            .WithMany()
            .HasForeignKey(p => p.PromotionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
