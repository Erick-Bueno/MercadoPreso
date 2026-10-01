using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Infrastructure.Context.Converters;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Infrastructure.Context;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Promotion> Promotions { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<ProductId>().HaveConversion<ProductIdConverter>();
        configurationBuilder.Properties<PromotionId>().HaveConversion<PromotionIdConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}
