using Modules.Catalog.Domain.Products;

namespace Modules.Catalog.Application.Products.Interfaces;

public interface IProductRepository
{
    public Task<Product> GetProductById(ProductId id);
}