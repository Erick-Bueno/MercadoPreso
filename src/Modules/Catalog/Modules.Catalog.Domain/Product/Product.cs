using Common.Domain;

namespace Modules.Catalog.Domain.Product;

public class Product : Entity
{
    public string Name { get; private set; }
    public Price Price { get; private set; }
}
