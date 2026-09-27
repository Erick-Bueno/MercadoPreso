using Common.Domain;
using Common.Endpoints;
using FastEndpoints;
using Modules.Catalog.Application.Promotions.DeactivatePromotion;
using Commands = Common.Application.Interfaces;
namespace Modules.Catalog.Endpoints.Promotions;

public class DeactivatePromotionEndpoint(Commands.ICommandHandler<DeactivatePromotionCommand, Unit> handler) : Endpoint<DeactivatePromotionCommand, Unit>
{
    public override void Configure()
    {
        Patch("promotion/{PromotionId:guid}/deactivate");
        Group<CatalogGroup>();
    }

    public override async Task HandleAsync(
    DeactivatePromotionCommand command,
    CancellationToken ct)
    {
        var result = await handler.Handle(command, ct);
        await Send.Respond(result, ct);
    }
}