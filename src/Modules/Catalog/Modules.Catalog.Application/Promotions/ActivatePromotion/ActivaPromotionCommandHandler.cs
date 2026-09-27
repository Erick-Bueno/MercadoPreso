using Common.Application.Interfaces;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Promotions.Interfaces;
using Modules.Catalog.Domain.Errors;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Application.Promotions.ActivatePromotion;

public class ActivaPromotionCommandHandler(IPromotionRepository promotionRepository, [FromKeyedServices("catalog")] IUnitOfWork unitOfWork) 
: ICommandHandler<ActivatePromotionCommand, Unit>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Unit>> Handle(ActivatePromotionCommand command, CancellationToken cancellationToken)
    {
        var promotionId = PromotionId.Create(command.PromotionId);
        var promotion = await _promotionRepository.GetPromotionById(promotionId);
        if (promotion is null)
        {
            return PromotionErrors.PromotionNotExists;
        }

        promotion.Activate();
        await _unitOfWork.SaveChanges(cancellationToken);
        return Unit.Value;
    }
}