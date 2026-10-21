using Common.Application.Interfaces;
using Common.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Infrastructure.Context;


namespace Modules.Catalog.Infrastructure;

public static class InfrastructureDependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCatalogInfrastructure(IConfiguration configuration)
        {
            services.AddScoped<OutboxInterceptor>();
            services.AddDbContext<CatalogDbContext>((sp, options) =>
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("default"),
                        o => o.MigrationsHistoryTable("__EFMigrationsHistory", "catalog")
                    )
                    .AddInterceptors(sp.GetRequiredService<OutboxInterceptor>())
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

            services.AddKeyedScoped<IOutboxWriter, OutboxWriter<CatalogDbContext>>("Catalog");
            return services;
        }
    }
}
