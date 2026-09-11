using Common.Domain;
using Modules.Catalog.Application.Promotions.Requests;
using Modules.Catalog.Application.Promotions.Responses;

namespace Modules.Catalog.Application.Promotions.Interfaces;

public interface IPromotionService
{
    public Task<Result<AddProductToPromotionResponse>> AddProductToPromotion(AddProductToPromotionRequest request, CancellationToken cancellationToken);
}