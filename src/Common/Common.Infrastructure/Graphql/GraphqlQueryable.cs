using System.Globalization;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Graphql;

public class Graphqlqueryable<TEntity, TContext> : ObjectTypeExtension<Query>
where TEntity : class
where TContext : DbContext
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        string fieldName = typeof(TEntity).Name.ToLower(CultureInfo.CurrentCulture);
        descriptor.
            Field(fieldName)
            .Resolve(context => context.Service<TContext>().Set<TEntity>())
            .UsePaging(options: new PagingOptions { IncludeTotalCount = true })
            .UseFiltering()
            .UseProjection()
            .UseSorting();

    }
}