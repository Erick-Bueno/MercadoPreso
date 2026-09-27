using System.Net;
using Common.Domain.Enums;

namespace Common.Domain.Errors;

public sealed record DomainError(HttpStatusCode Code, string Message, ErrorType Type)
{
    public static readonly DomainError None = new(HttpStatusCode.NotImplemented, string.Empty, ErrorType.None);
    public static readonly DomainError ValueCannotBeNegative = new(HttpStatusCode.BadRequest, "Valor não pode ser negativo", ErrorType.Validation);

}
