using Common.Infrastructure.Context;
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

           return services;
        }
    }
}