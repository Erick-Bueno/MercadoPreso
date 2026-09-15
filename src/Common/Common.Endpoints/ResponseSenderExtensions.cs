using FastEndpoints;

namespace Common.Endpoints;

public static class ResponseSenderExtensions
{
    extension<TRequest, TResponse>(ResponseSender<TRequest, TResponse> responseSender)
        where TRequest : notnull
    {
        
    }
}
