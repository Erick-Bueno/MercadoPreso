using Common.Application.Interfaces;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Products.Interfaces;
using Modules.Catalog.Application.Promotions.Interfaces;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Products;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Application.Products.RemovePromotionFromProduct;

public class RemovePromotionFromProductCommandHandler(IPromotionRepository promotionRepository, IProductRepository productRepository, [FromKeyedServices("catalog")] IUnitOfWork unitOfWork) : ICommandHandler<RemovePromotionFromProductCommand, Unit>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<Unit>> Handle(RemovePromotionFromProductCommand command, CancellationToken cancellationToken)
    {
        var promotionId = PromotionId.Create(command.PromotionId);
        var promotion = await _promotionRepository.GetPromotionById(promotionId);
        if (promotion is null)
        {
            return PromotionErrors.PromotionNotExists;
        }
        var productId = ProductId.Create(command.ProductId);
        var product = await _productRepository.GetProductById(productId);
        if (product is null)
        {
            return ProductErrors.ProductNotExists;
        }

        if (product.ActivatePromotion(promotionId) is { IsFailure: true } result)
        {
            return result.Error;
        }

        await _unitOfWork.SaveChanges(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}