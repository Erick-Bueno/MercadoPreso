namespace Common.Domain.Errors;

public record ValueCannotBeNegative() : DomainError("Valor não pode ser negativo");