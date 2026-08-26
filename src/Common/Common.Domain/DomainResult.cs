using Common.Domain.Errors;

namespace Common.Domain;

public sealed class DomainResult<T>
{
    public T? Success { get; private set; } 
    public DomainError? Failure {get; private set;}
    public DomainResult(T value){
        Success = value;
    }
    public DomainResult(DomainError error){
        Failure = error;
    }

    public static implicit operator DomainResult<T> (T value) => new(value);
    public static implicit operator DomainResult<T> (DomainError error) => new(error);
}
