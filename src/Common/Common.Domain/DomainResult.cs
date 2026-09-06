using Common.Domain.Errors;

namespace Common.Domain;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public DomainError? Error { get; }
    public static Result Success { get; } = new();

    protected Result() => IsSuccess = true;

    protected Result(DomainError error)
    {
        Error = error;
        IsSuccess = false;
    }

    public static implicit operator Result(DomainError error) => new(error);

}

public sealed class Result<T> : Result
{
    private readonly T? _value;

    private Result(T value)
    {
        _value = value;
    }
    private Result(DomainError error) : base(error) { }

    public T Value =>
        IsSuccess
            ? _value!
            : throw new InvalidOperationException(
                "Não é possível acessar o valor de um resultado com falha."
            );


    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(DomainError error) => new(error);
}
