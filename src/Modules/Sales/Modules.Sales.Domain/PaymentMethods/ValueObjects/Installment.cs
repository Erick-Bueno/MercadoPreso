using Common.Domain;

namespace Modules.Sales.Domain.PaymentMethods.ValueObjects;

public record Installment(
    int Count, 
    Price Amount,
    Price TotalAmount
);