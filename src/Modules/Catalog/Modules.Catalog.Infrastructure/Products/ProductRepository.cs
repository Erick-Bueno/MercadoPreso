using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Products.Interfaces;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Infrastructure.Context;

namespace Modules.Catalog.Infrastructure.Products;

public class ProductRepository(CatalogDbContext dbContext) : IProductRepository
{
    private readonly CatalogDbContext _dbContext = dbContext;

    public async Task<Product?> GetProductById(ProductId id, CancellationToken cancellationToken) =>
        await _dbContext.Products.FirstOrDefaultAsync(
            product => product.Id == id,
            cancellationToken
        );
}
