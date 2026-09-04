using Common.Domain;
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
        
        builder.Property(p => p.Price)
            .HasConversion(price => price.Value, value => Price.Create(value).Value);

        builder
            .HasOne<Promotion>()
            .WithMany()
            .HasForeignKey(p => p.PromotionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
