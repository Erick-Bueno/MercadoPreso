using Common.Application.Interfaces;

namespace Modules.Catalog.Application.Products.RegisterProduct;

public sealed record RegisterProductCommand(
    string Name,
    decimal Price
) : ICommand<RegisterProductResponse>;

