using Common.Domain.Errors;

namespace Common.Domain;

public static class ResultExtensions
{
    extension<T>(Result<T> result)
    {
        public Result<T> Ensure(Func<T, bool> predicate, DomainError error)
        {
            if (result.IsFailure)
            {
                return error;
            }

            return predicate(result.Value) ? result : error;
        }
        public Result<TOut> Map<TOut>(
             Func<T, TOut> mappingFunc
         )
         => result.IsSuccess ? mappingFunc(result.Value) : result.Error;
        

        public Result<T> Tap(Action<T> action)
        {
            if(result.Error is not null)
            {
                return result.Error;
            }
            action()
        }

    }
}