using Common.Application.Interfaces;

namespace Modules.Catalog.Application.Products.RegisterProduct.cs;

public sealed record RegisterProductCommand(
    string Name,
    decimal Price,
    Guid? PromotionId
) : ICommand<RegisterProductResponse>;

