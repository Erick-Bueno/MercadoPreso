/* using Common.Domain.Errors;

namespace Common.Domain.Extensions;

public static class DomainResultExtension
{
    extension<T>(DomainResult<T> result)
    {
        public DomainResult<T> Ensure(Func<T?, bool> predicate, DomainError error)
        {
            if (result.IsFailure)
            {
                return result;
            }
            return predicate(result.Value) ? result : error;
        }
    }
}
  */