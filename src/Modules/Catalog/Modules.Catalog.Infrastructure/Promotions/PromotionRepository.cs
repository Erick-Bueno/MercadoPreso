using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Promotions.Interfaces;
using Modules.Catalog.Domain.Promotions;
using Modules.Catalog.Infrastructure.Context;

namespace Modules.Catalog.Infrastructure.Promotions;

public class PromotionRepository(CatalogDbContext dbContext) : IPromotionRepository
{
    private readonly CatalogDbContext _dbContext = dbContext;

    public async Task<Promotion?> GetPromotionById(PromotionId promotionId) =>
        await _dbContext.Promotions.FirstOrDefaultAsync(promotion => promotion.Id == promotionId);
}
