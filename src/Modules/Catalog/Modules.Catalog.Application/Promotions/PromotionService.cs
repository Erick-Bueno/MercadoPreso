using Common.Domain;
using Modules.Catalog.Application.Products.Interfaces;
using Modules.Catalog.Application.Promotions.Interfaces;
using Modules.Catalog.Application.Promotions.Requests;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Application.Promotions;

public class PromotionService(IPromotionRepository promotionRepository, IProductRepository productRepository) : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly

    public async Result<Task> AddProductToPromotion(AddProductToPromotionRequest request)
    {
        var promotionId = PromotionId.Create(request.PromotionId);
        var promotion = _promotionRepository.GetPromotionById(promotionId);
        if(promotion is null)
        {
            return PromotionErrors.PromotionNotExists;
        }
        var productId = ProductId.Create(request.ProductId);
        var product = await _productRepository.GetProductById(productId);
        if(product is null)
        {
            return ProductErrors.ProductNotExists;
        }
        product.ActivatePromotion(promotionId);
    }
}