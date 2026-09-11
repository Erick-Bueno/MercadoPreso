using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Application.Promotions.Responses;

public record AddProductToPromotionResponse(string ProductName, string? PromotionDescription, string PromotionTitle, Discount Discount, Period Period);