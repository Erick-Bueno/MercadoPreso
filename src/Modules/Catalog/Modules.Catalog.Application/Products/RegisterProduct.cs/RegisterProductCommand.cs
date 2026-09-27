using Common.Application.Interfaces;
using Common.Domain;

namespace Modules.Catalog.Application.Products.RegisterProduct.cs;

public sealed record RegisterProductCommand(
    string Name,
    decimal Price,
    Guid? PromotionId
) : ICommand<RegisterProductResponse>;

