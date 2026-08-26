using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SSO.Requests;

public record CreateClientRequest(
    string Id,
    string DisplayName,
    HashSet<Uri> RedirectUris
);