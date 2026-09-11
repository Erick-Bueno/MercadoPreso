using System.Runtime.CompilerServices;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Application.Promotions.Interfaces;

public interface IPromotionRepository
{
    public Task<Promotion?> GetPromotionById(PromotionId promotionId);
}