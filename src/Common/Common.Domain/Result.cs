using Common.Domain.Errors;

namespace Common.Domain;



public readonly union Result<T>(T, DomainError);