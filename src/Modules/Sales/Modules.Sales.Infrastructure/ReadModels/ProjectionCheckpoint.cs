namespace Modules.Sales.Infrastructure.ReadModels;

public class ProjectionCheckpoint
{
    public string Name { get; set; } = string.Empty;
    public long Position { get; set; }
}