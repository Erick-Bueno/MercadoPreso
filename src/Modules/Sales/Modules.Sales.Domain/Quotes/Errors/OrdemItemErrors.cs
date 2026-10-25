using System.Net;
using Common.Domain.Enums;
using Common.Domain.Errors;

namespace Modules.Sales.Domain.Quotes.Errors;

public sealed record OrderItemErrors
{
    public static readonly DomainError QuantityShouldNotBeNegative = new(HttpStatusCode.BadRequest, "Quantidade inválida", ErrorType.Validation);

}