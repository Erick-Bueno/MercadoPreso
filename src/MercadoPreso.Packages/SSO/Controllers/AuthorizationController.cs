using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SSO.Controllers;

public class AuthorizationController : Controller
{
    /* [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange() { } */

    [HttpGet("~/connect/authorize")]
    [HttpPost("~/connect/authorize")]
    public async Task<IActionResult> Authorize()
    {
        //obtem a request do openidicct estruturada
        var request =
            HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException(
                "The OpenID Connect request cannot be retrieved."
            );
        //le e valida o cookie de sessao
        var sessionCookie = await HttpContext.AuthenticateAsync();

        //validacao do prompt none
        if (request.HasPromptValue(PromptValues.None))
        {
            return Forbid(
                properties: new AuthenticationProperties(
                    new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] =
                            Errors.LoginRequired,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The user is not logged in.",
                    }
                )
            );
        }

        if (
            sessionCookie is not { Succeeded: true }
            || (
                (
                    request.HasPromptValue(PromptValues.Login)
                    || request.MaxAge is 0
                    || (
                        request.MaxAge is not null
                        && sessionCookie.Properties?.IssuedUtc is not null
                        && TimeProvider.System.GetUtcNow() - sessionCookie.Properties.IssuedUtc
                            > TimeSpan.FromSeconds(request.MaxAge.Value)
                    )
                ) && TempData["IgnoreAuthenticationChallenge"] is null or false
            )
        )
        {
            //evitar loop infinito de logins
            TempData["IgnoreAuthenticationChallenge"] = true;
            //se n tiver cookie de sessao valido retorna pra pagina de login
            return Challenge(
                authenticationSchemes: CookieAuthenticationDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri =
                        Request.PathBase
                        + Request.Path
                        + QueryString.Create(
                            Request.HasFormContentType ? Request.Form : Request.Query
                        ),
                }
            );
        };

        return Ok();
    }
}
