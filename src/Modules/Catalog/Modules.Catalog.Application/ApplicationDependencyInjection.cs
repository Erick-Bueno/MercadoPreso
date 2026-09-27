using Common.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Modules.Catalog.Application;

public static class ApplicationDependencyInjection
{
    extension(IServiceCollection services)
    {
    public IServiceCollection AddCatalogApplication()
        {
            services.Scan(selector =>
            selector.FromAssemblies(typeof(ApplicationDependencyInjection).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .As(classType => classType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
                .WithScopedLifetime());
            return services;
        }
    }
}