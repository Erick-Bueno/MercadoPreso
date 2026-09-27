using Commands = Common.Application.Interfaces;
using Common.Endpoints;
using FastEndpoints;
using Modules.Catalog.Application.Products.AddProductToPromotion.cs;

namespace Modules.Catalog.Endpoints.Products;

public class AddProductToPromotionEndpoint(Commands.ICommandHandler<AddProductToPromotionCommand, AddProductToPromotionResponse> handler)
    : Endpoint<AddProductToPromotionCommand, AddProductToPromotionResponse>
{
    public override void Configure()
    {
        Post("promotions/{PromotionId:guid}/products/{ProductId:guid}");
        Group<CatalogGroup>();
    }

    public override async Task HandleAsync(
        AddProductToPromotionCommand command,
        CancellationToken ct
    )
    {
        var result = await handler.Handle(command, ct);
        await Send.Respond(result, ct);
    }
}
