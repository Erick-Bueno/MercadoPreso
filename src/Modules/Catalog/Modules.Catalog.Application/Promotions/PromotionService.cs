using Common.Application.Interfaces;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Products.Interfaces;
using Modules.Catalog.Application.Promotions.Interfaces;
using Modules.Catalog.Application.Promotions.Requests;
using Modules.Catalog.Application.Promotions.Responses;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Application.Promotions;

public class PromotionService(
    IPromotionRepository promotionRepository,
    IProductRepository productRepository,
    [FromKeyedServices("catalog")] IUnitOfWork unitOfWork
) : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AddProductToPromotionResponse>> AddProductToPromotion(
        AddProductToPromotionRequest request,
        CancellationToken cancellationToken
    )
    {
        var promotionId = PromotionId.Create(request.PromotionId);
        var promotion = await _promotionRepository.GetPromotionById(promotionId);
        if (promotion is null)
        {
            return PromotionErrors.PromotionNotExists;
        }
        var productId = ProductId.Create(request.ProductId);
        var product = await _productRepository.GetProductById(productId);
        if (product is null)
        {
            return ProductErrors.ProductNotExists;
        }
        product.ActivatePromotion(promotionId);

        await _unitOfWork.SaveChanges(cancellationToken: cancellationToken);

        return new AddProductToPromotionResponse(
            product.Name,
            promotion.Description,
            promotion.Title,
            promotion.Discount,
            promotion.Period
        );
    }
}
