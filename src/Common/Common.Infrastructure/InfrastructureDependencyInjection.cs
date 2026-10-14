using Common.Infrastructure.Context;
using Common.Infrastructure.Messaging.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;

public static class InfrastructureDependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCommonInfrastructure(IConfiguration configuration)
        {
            services.AddOptions<ConsumerOptions>(ConsumerOptions.SectionName)
            .Bind(configuration.GetSection(ConsumerOptions.Catalog));
            return services;
        }
    }
}