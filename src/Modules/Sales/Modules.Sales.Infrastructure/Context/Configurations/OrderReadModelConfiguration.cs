using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Sales.Infrastructure.ReadModels;

namespace Modules.Sales.Infrastructure.Context.Configurations;

public class OrderReadModelConfiguration : IEntityTypeConfiguration<OrderReadModel>
{
    public void Configure(EntityTypeBuilder<OrderReadModel> builder)
    {
        builder.HasKey(order => order.Id);
        builder.Property(order => order.TotalPrice).HasPrecision(18, 2);
    }
}