namespace Modules.Catalog.Application.Promotions.Requests;

public sealed record AddProductToPromotionRequest(Guid ProductId, Guid PromotionId);