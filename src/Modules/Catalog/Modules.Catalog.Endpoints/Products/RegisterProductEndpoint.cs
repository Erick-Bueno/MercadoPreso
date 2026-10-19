using FastEndpoints;
using Modules.Catalog.Application.Products.RegisterProduct;

namespace Modules.Catalog.Endpoints.Products;

public class RegisterProductEndpoint : Endpoint<RegisterProductCommand,RegisterProductResponse>
{
    public override void Configure()
    {
        base.Configure();
    }
}