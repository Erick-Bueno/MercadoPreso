using Common.Application.Interfaces;
using Modules.Catalog.Infrastructure.Context;

namespace Modules.Catalog.Infrastructure;

public class UnitOfWork(CatalogDbContext dbContext) : IUnitOfWork
{
    private readonly CatalogDbContext _dbContext = dbContext;

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
