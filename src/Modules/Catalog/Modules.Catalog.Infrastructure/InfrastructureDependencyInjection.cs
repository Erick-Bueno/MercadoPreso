using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Infrastructure.Context;

namespace Modules.Catalog.Infrastructure;

public static class InfrastructureDependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCatalogModule(IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("default"),
                o => o.MigrationsHistoryTable("__EFMigrationsHistory", "catalog"))
            );

            return services;
        }
    }
}