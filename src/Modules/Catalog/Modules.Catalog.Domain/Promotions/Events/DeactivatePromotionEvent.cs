using Common.Domain;

namespace Modules.Catalog.Domain.Promotions.Events;

public record DeactivatePromotionEvent(PromotionId PromotionId) : DomainEvent();