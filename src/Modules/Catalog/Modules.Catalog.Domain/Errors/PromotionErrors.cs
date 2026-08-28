using Common.Domain.Errors;

namespace Modules.Catalog.Domain.Errors;


public sealed record PromotionErrors()
{
    public static readonly DomainError ProductLimitReached = new("Limite de produtos atingido");
    public static readonly DomainError ProductAlreadyHasAnActivePromotion = new("O produto ja possui uma promoção ativa");
    public static readonly DomainError EndDateCannotBeBeforeStartDate = new("A data final não pode ser antes da data inicial");
}