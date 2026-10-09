using Microsoft.EntityFrameworkCore.Design;

namespace Modules.Catalog.Infrastructure.Context;

public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(string[] args)
    {
        
    }
}