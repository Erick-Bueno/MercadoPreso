using Common.Domain;
using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Products;

public class Product : Entity
{

    public string Name { get; private set; } = string.Empty;
    public Price Price { get; private set; }
    public Guid? PromotionId { get; private set; }

    private Product(string name, Price price)
    {
        Name = name;
        Price = price;
    }

    public static DomainResult<Product> Create(string name, Price price)
    {
        if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return new ProductNameIsRequired();
        }
        return new Product(name, price);
    }
}
