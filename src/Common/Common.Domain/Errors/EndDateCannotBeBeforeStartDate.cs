namespace Common.Domain.Errors;

public record EndDateCannotBeBeforeStartDate() : DomainError("A data final não pode ser após a data inicial");