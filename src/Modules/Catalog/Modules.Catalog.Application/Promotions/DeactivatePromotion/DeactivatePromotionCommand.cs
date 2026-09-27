using System.Windows.Input;
using Common.Application.Interfaces;
using Common.Domain;

namespace Modules.Catalog.Application.Promotions.DeactivatePromotion;

public record DeactivatePromotionCommand(Guid PromotionId) : ICommand<Unit>;