using Common.Infrastructure.Graphql;
using Modules.Catalog.Infrastructure;

namespace MercadoPreso.Api.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddGraphql()
        {
            services
                .AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<Query>()
                .AddFiltering()
                .AddSorting()
                .AddProjections()
                .AddCatalogGraphql();
            
            return services;
        }
    }
}