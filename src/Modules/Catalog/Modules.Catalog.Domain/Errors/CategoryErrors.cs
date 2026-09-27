using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Errors;


public sealed record CategoryErrors
{
    public static readonly DomainError CategoryNameIsRequired = new(System.Net.HttpStatusCode.BadRequest, "O Nome da categoria é obrigatório", Common.Domain.Enums.ErrorType.Validation);
}