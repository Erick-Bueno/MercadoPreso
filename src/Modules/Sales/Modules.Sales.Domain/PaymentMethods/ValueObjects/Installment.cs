using Common.Domain.ValueObjects;

namespace Modules.Sales.Domain.PaymentMethods.ValueObjects;

public sealed record Installment
{
    public int Count { get; private set; }
    public Price Amount { get; private set; }
    public Price TotalAmount { get; private set; }

    private Installment(int count, Price amount, Price totalAmount)
    {
        Count = count;
        Amount = amount;
        TotalAmount = totalAmount;
    }

    public static Installment Create(int count, Price amount, Price totalAmount) =>
        new(count, amount, totalAmount);
}
