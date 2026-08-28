using Common.Domain;

namespace Modules.Catalog.Domain.Products;

public class Image : Entity
{
    public Guid ProductId { get; private set; }
    public Uri? Url { get; set; }

}
