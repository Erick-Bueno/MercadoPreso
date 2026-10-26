namespace Common.Domain;

public interface IEntity
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public object GetId();
}
