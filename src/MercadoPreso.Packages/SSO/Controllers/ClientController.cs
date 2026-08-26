using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using SSO.Requests;
using SSO.Responses;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SSO.Controllers;

public class ClientController(IOpenIddictApplicationManager applicationManager) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateClientRequest request)
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var descriptor = new OpenIddictApplicationDescriptor
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
            DisplayName = request.DisplayName,
            Permissions =
            {
                Permissions.Endpoints.Authorization,
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.AuthorizationCode,
                Permissions.ResponseTypes.Code,
                Permissions.Scopes.Email,
                Permissions.Scopes.Profile,
            },
            ApplicationType = ApplicationTypes.Web
        };

        foreach (var uri in request.RedirectUris)
        {
            descriptor.RedirectUris.Add(
                uri
            );
        }

        await applicationManager.CreateAsync(descriptor);

        return Created("", new CreateClientResponse(clientId, clientSecret));
    }

    [HttpGet]
    public IActionResult Redirect()
    {
        return Content("Home, Bem vindo Erick");
    }
}
