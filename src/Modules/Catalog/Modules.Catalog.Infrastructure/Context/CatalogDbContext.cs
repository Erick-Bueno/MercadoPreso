using Common.Domain;
using Common.Infrastructure.Context;
using Common.Infrastructure.Context.Configurations;
using Common.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Domain.Promotions;
using Modules.Catalog.Infrastructure.Context.Converters;

namespace Modules.Catalog.Infrastructure.Context;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Outbox> Outbox { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<ProductId>().HaveConversion<ProductIdConverter>();
        configurationBuilder.Properties<PromotionId>().HaveConversion<PromotionIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new OutboxConfiguration());

        modelBuilder.ApplyAuditDates();
    }
}
