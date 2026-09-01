using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Catalog.Domain.Products;

namespace Modules.Catalog.Infrastructure.Context.Converters;

public class ProductIdConverter()
    : ValueConverter<ProductId, Guid>(id => id.Value, value => new ProductId(value));
