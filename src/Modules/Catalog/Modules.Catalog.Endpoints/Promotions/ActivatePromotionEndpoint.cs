using Common.Domain;
using Common.Endpoints;
using FastEndpoints;
using Modules.Catalog.Application.Promotions.ActivatePromotion;
using Commands = Common.Application.Interfaces;
namespace Modules.Catalog.Endpoints.Promotions;

public class ActivatePromotionEndpoint(Commands.ICommandHandler<ActivatePromotionCommand, Unit> handler) 
: Endpoint<ActivatePromotionCommand, Unit>
{
    public override void Configure()
    {
        Patch("catalog/promotion/{id:guid}/activate");
    }

    public override async Task HandleAsync(
        ActivatePromotionCommand command,
        CancellationToken ct)
    {
        var result = await handler.Handle(command, ct);
        await Send.Respond(result, ct);
    }

}