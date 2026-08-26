using Common.Domain;

namespace Modules.Catalog.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; } = string.Empty;
    public Price? Price { get; private set; }
}
