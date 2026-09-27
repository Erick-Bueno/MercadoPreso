using Common.Domain;
using Modules.Catalog.Application.Products.AddProductToPromotion.cs;
using Modules.Catalog.Application.Promotions.Requests;

namespace Modules.Catalog.Application.Promotions.Interfaces;

public interface IPromotionService
{
    public Task<Result<AddProductToPromotionResponse>> AddProductToPromotion(AddProductToPromotionRequest request, CancellationToken cancellationToken);
    public Task<Result<Unit>> ActivatePromotion(ActivatePromotionRequest request, CancellationToken cancellationToken); 
}