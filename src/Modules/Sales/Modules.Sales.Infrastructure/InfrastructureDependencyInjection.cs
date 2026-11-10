using Microsoft.Extensions.DependencyInjection;

namespace Modules.Sales.Infrastructure;

public static class InfrastructureDependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddSalesInfrastructure()
        {
            services.AddKurrentDBClient();

            
            return services;
        }
    }
}