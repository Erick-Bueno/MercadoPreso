namespace SSO.Responses;

public record CreateClientResponse(
    string ClientId,
    string ClientSecret
);