using Common.Application.Interfaces;
using Common.Domain;

namespace Modules.Catalog.Application.Promotions.ActivatePromotion;

public record ActivatePromotionCommand(
    Guid PromotionId
): ICommand<Unit>;