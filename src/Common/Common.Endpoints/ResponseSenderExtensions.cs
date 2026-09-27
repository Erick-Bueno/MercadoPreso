using Common.Domain;
using Common.Domain.Errors;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Common.Endpoints;

public static class ResponseSenderExtensions
{
    extension<TRequest, TResponse>(ResponseSender<TRequest, TResponse> sender)
        where TRequest : notnull
    {
        public Task Respond(Result<TResponse> result, CancellationToken cancellationToken)
        =>
            result.Match(
                value => sender.OkAsync(value, cancellation: cancellationToken),
                error => sender.ResultAsync(ToProblem(error))
            );


    }
    private static IResult ToProblem(DomainError error) =>
    Results.Problem(
        detail: error.Message,
        statusCode: (int)error.Code,
        type: error.Type.ToString()
    );
}
