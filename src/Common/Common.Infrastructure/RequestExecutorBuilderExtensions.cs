using System.Reflection;
using Common.Infrastructure.Graphql;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;

public static class RequestExecutorBuilderExtensions
{
    extension(IRequestExecutorBuilder builder)
    {
        public IRequestExecutorBuilder AddGraphqlToEntities<TContext>() where TContext : DbContext
        {
            var entities = typeof(TContext)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .Select(p => p.PropertyType.GetGenericArguments()[0]);


            foreach (var entity in entities)
            {
                var extension = typeof(Graphqlqueryable<,>)
                    .MakeGenericType(entity, typeof(TContext));
                builder.AddTypeExtension(extension);
            }
            return builder;
        }
    }
}