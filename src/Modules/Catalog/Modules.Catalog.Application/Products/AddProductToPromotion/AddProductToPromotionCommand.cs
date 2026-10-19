using Common.Application.Interfaces;

namespace Modules.Catalog.Application.Products.AddProductToPromotion.cs;

public sealed record AddProductToPromotionCommand(Guid ProductId, Guid PromotionId) : ICommand<AddProductToPromotionResponse>;