namespace Common.Domain.Errors;

public record ProductLimitReachedForThisPromotion() : DomainError("Limite de produtos atingido para esta promoção");