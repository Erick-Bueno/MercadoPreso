namespace Common.Domain.Errors;

public sealed record DomainError(string Description)
{
    public static readonly DomainError None = new(string.Empty);
    public static readonly DomainError ValueCannotBeNegative = new("Valor não pode ser negativo");

}
