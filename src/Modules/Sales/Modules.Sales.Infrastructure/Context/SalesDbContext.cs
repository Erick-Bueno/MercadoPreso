using Microsoft.EntityFrameworkCore;
using Modules.Sales.Infrastructure.ReadModels;

namespace Modules.Sales.Infrastructure.Context;


public class SalesDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<OrderReadModel> Orders { get; set; }
    public DbSet<ProjectionCheckpoint> ProjectionCheckpoints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        modelBuilder.HasDefaultSchema("Sales");

    }
}