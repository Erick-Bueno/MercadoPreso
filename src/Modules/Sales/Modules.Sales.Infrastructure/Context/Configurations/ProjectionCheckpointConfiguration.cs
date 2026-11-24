using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Sales.Infrastructure.ReadModels;

namespace Modules.Sales.Infrastructure.Context.Configurations;


public class ProjectionCheckpointConfiguration : IEntityTypeConfiguration<ProjectionCheckpoint>
{
    public void Configure(EntityTypeBuilder<ProjectionCheckpoint> builder)
    {
        builder.HasKey(checkpoint => checkpoint.Name);
        builder.Property(checkpoint => checkpoint.Name).HasMaxLength(200);
    }
}