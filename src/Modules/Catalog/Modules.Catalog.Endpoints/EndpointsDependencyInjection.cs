using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Catalog.Endpoints;

public static class EndpointsDependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCatalogEndpoints()
        {
            services.AddFastEndpoints();
            return services;
        }
    }
}