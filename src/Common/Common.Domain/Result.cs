using Common.Domain.Errors;

namespace Common.Domain;

public static class Result
{
    public static Result<Unit> Success { get; } = Unit.Value;

    public static Result<T> Create<T>(T value) => new(value);
}

public sealed class Result<T>
{
    private readonly T? _value;
    private readonly DomainError? _error;
    public bool IsSuccess => _error is null;
    public bool IsFailure => _error is not null;

    public Result(T value)
    {
        _value = value;
    }

    public Result(DomainError? error)
    {
        _error = error;
    }

    public T Value =>
        IsSuccess
            ? _value!
            : throw new InvalidOperationException(
                "Não é possível acessar o valor de um resultado com falha."
            );

    public DomainError Error =>
        !IsSuccess
            ? _error!
            : throw new InvalidOperationException(
                "Não é possível acessar o valor de um erro inexistente"
            );

    public static implicit operator Result<T>(T value) => new(value);

    public static implicit operator Result<T>(DomainError? error) => new(error);

    public TMatch Match<TMatch>(Func<T, TMatch> onSuccess, Func<DomainError, TMatch> onError)
        => IsSuccess ? onSuccess(Value) : onError(Error);
}
