using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Modules.Catalog.Infrastructure.Context;

public sealed class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    private const string ConnectionString =
        "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=mercado_preso;";

    public CatalogDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseNpgsql(
                ConnectionString,
                o => o.MigrationsHistoryTable("__EFMigrationsHistory", "catalog")
            )
            .Options;

        return new CatalogDbContext(options);
    }
}
