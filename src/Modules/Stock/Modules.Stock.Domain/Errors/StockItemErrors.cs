using System.Net;
using Common.Domain.Enums;
using Common.Domain.Errors;

namespace Modules.Stock.Domain.Errors;

public sealed record StockItemErrors()
{
    public static readonly DomainError AvailableStockCannotBeNegative = new(
        HttpStatusCode.BadRequest,
        "Estoque disponivel não pode ser negativo",
        ErrorType.Validation
    );
}
