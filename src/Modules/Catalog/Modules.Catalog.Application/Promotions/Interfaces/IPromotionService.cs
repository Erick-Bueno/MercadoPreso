using Modules.Catalog.Application.Promotions.Requests;

namespace Modules.Catalog.Application.Promotions.Interfaces;

public interface IPromotionService
{
    public Task AddProductToPromotion(AddProductToPromotionRequest request);
}