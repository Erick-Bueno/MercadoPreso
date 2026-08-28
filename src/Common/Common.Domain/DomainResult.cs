using Common.Domain.Errors;

namespace Common.Domain;

public sealed class DomainResult<T>
{
    private readonly T? _value;
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public DomainError? Error { get; }

    private DomainResult(T value)
    {
        _value = value;
        IsSuccess = true;
    }
    private DomainResult(DomainError error)
    {
        Error = error;
        IsSuccess = false;
    }

    public T? Value =>
        IsSuccess
            ? _value
            : throw new InvalidOperationException(
                "Não é possível acessar o valor de um resultado com falha."
            );    


    public static implicit operator DomainResult<T>(T value) => new(value);

    public static implicit operator DomainResult<T>(DomainError error) => new(error);
}
