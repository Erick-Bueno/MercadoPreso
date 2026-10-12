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
            services.AddDbContext<CommonDbContext>(
               options => options.UseNpgsql(configuration.GetConnectionString("default"),
               o => o.MigrationsHistoryTable("__EFMigrationsHistory", "common"))
           );

            services.AddOptions<ConsumerOptions>(ConsumerOptions.SectionName)
            .Bind(configuration.GetSection(ConsumerOptions.Catalog));
            return services;
        }
    }
}