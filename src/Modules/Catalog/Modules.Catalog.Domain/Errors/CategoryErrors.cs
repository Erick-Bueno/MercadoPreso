using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Errors;


public sealed record CategoryErrors
{
    public static readonly DomainError CategoryNameIsRequired = new("O Nome da categoria é obrigatório");
}