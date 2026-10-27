namespace Modules.Sales.Domain.PaymentMethods.ValueObjects;

public sealed record Address(
    string Street, int Number, string ZipCode
);
