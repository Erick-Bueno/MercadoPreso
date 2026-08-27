namespace Common.Domain.Errors;

public record ProductNameIsRequired() : DomainError("O Nome do produto é obrigatório");