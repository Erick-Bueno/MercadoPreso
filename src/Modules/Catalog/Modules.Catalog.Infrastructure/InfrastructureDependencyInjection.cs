using Common.Application.Interfaces;
using Common.Infrastructure;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application;
using Modules.Catalog.Application.Products.Interfaces;
using Modules.Catalog.Application.Promotions.Interfaces;
using Modules.Catalog.Infrastructure.Context;
using Modules.Catalog.Infrastructure.Products;
using Modules.Catalog.Infrastructure.Promotions;

namespace Modules.Catalog.Infrastructure;

public static class InfrastructureDependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCatalogInfrastructure(IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("default"),
                    o => o.MigrationsHistoryTable("__EFMigrationsHistory", "catalog")
                )
            );
            services.AddKeyedScoped<IUnitOfWork, UnitOfWork>("catalog");

            services.Scan(selector =>
                selector
                    .FromAssemblies(typeof(InfrastructureDependencyInjection).Assembly)
                    .AddClasses(classes =>
                        classes.Where(c =>
                            c.Name.EndsWith("Repository", StringComparison.Ordinal)
                        )
                    )
                    .AsMatchingInterface()
                    .WithScopedLifetime()
            );
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
