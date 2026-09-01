using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Catalog.Domain.Promotions;

namespace Modules.Catalog.Infrastructure.Context.Converters;

public class PromotionIdConverter()
    : ValueConverter<PromotionId, Guid>(id => id.Value, value => new PromotionId(value));
