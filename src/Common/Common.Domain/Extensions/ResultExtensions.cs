using Common.Domain.Errors;

namespace Common.Domain.Extensions;

public static class ResultExtensions
{
    extension<T>(Result<T> result)
    {
        public TMatch Match<TMatch>(Func<T, TMatch> onSuccess, Func<DomainError, TMatch> onError)
        => result switch
        {
            T value => onSuccess(value),
            DomainError error => onError(error),
            null => throw new InvalidOperationException(),

        };
        public Result<T> Ensure(Func<T, bool> predicate, DomainError error)
            => result switch
            {
                T value => predicate(value) ? result : error,
                DomainError err => err,
                null => throw new InvalidOperationException()

            };
        public Result<TOut> Map<TOut>(
             Func<T, TOut> mappingFunc
         )
         => result switch
         {
             T value => mappingFunc(value),
             DomainError error => error,
             null => throw new InvalidOperationException()

         };

        public Result<T> Tap(Action<T> action)
        {
            switch (result)
            {
                case T value:
                    action(value);
                    return result;
                case DomainError error:
                    return error;
                default:
                    throw new InvalidOperationException();
            }
            ;
        }
        public Result<TOut> Bind<TOut>(Func<T, Result<TOut>> func)
        => result switch
        {
            T value => func(value),
            DomainError error => error,
            null => throw new InvalidOperationException()

        };
    }

}

public static class Result
{
    public static Result<Unit> Success { get; } = Unit.Value;
    public static Result<T> Create<T>(T value) => new(value);
    public static Result<(T1, T2)> Combine<T1, T2>(Result<T1> result1, Result<T2> result2)

     => (result1, result2) switch
     {
         (T1 value1, T2 value2) => (value1, value2),
         (DomainError error1, _) => error1,
         (_, DomainError error2) => error2,
         _ => throw new InvalidOperationException()
     };

    public static Result<(T1, T2, T3)> Combine<T1, T2, T3>(Result<T1> result1, Result<T2> result2, Result<T3> result3)

   => (result1, result2, result3) switch
   {
       (T1 value1, T2 value2, T3 value3) => (value1, value2, value3),
       (DomainError error1, _, _) => error1,
       (_, DomainError error2, _) => error2,
       (_, _, DomainError error3) => error3,
       _ => throw new InvalidOperationException()
   };

    public static Result<List<T>> Combine<T>(IEnumerable<Result<T>> results)
    {
        var values = new List<T>();

        foreach (var result in results)
        {
            switch (result)
            {
                case T value:
                    values.Add(value);
                    break;
                case DomainError error:
                    return error;
                default:
                    throw new InvalidOperationException();
            }
        }
        return values;
    }

}
