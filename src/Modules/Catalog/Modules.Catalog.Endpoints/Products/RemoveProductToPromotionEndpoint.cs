using Commands = Common.Application.Interfaces;
using Common.Endpoints;
using FastEndpoints;
using Common.Domain;
using Modules.Catalog.Application.Products.RemovePromotionFromProduct;

namespace Modules.Catalog.Endpoints.Products;

public class RemovePromotionFromProductEndpoint(Commands.ICommandHandler<RemovePromotionFromProductCommand, Unit> handler)
    : Endpoint<RemovePromotionFromProductCommand, Unit>
{
    public override void Configure()
    {
        Delete("promotions/{PromotionId:guid}/products/{ProductId:guid}");
        Group<CatalogGroup>();
    }

    public override async Task HandleAsync(
        RemovePromotionFromProductCommand command,
        CancellationToken ct
    )
    {
        var result = await handler.Handle(command, ct);
        await Send.Respond(result, ct);
    }
}
