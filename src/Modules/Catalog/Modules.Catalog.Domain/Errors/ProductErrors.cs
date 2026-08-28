using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Errors;


public sealed record ProductErrors()
{
    public static readonly DomainError ProductNameIsRequired = new("O Nome do produto é obrigatório");
    public static readonly DomainError CannotAddTheSameImageToTheProduct = new("Não é possivel adicionar a mesma imagem a um produto");
    public static readonly DomainError ProductMustHaveAtLeastOneImage = new("O produto deve possuir pelo menos uma imagem");
}