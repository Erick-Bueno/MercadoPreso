using FastEndpoints;
using Modules.Catalog.Application.Promotions.Requests;
using Modules.Catalog.Application.Promotions.Responses;

namespace Modules.Catalog.Endpoints.Promotions;

public class AddProductToPromotionEndpoint: Endpoint<AddProductToPromotionRequest, AddProductToPromotionResponse>
{
    public override void Configure()
    {   
        Post("catalog/promotion");
        Version(1);
    }

    public override async Task HandleAsync(AddProductToPromotionRequest request, CancellationToken cancellationToken)
    {
        
    }
}