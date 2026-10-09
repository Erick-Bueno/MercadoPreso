using FastEndpoints;

namespace Modules.Catalog.Endpoints;


public class TesteEndpoint: Endpoint<EmptyRequest, string> 
{
    public override void Configure()
    {
        Get("/teste");
        AllowAnonymous();

    }
    public override Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        return Send.OkAsync("hello world", ct);
    }
}