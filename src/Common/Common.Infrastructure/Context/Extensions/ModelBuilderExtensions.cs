using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Context;

public static class ModelBuilderExtensions
{
    extension(ModelBuilder builder)
    {
        public ModelBuilder ApplyAuditDates()
        {
            var entities = builder
                .Model.GetEntityTypes()
                .Where(t => typeof(IEntity).IsAssignableFrom(t.ClrType))
                .ToList();

            foreach (var entity in entities)
            {
                var modelBuilder = builder.Entity(entity.ClrType);

                modelBuilder
                    .Property(nameof(IEntity.CreatedAt))
                    .HasDefaultValueSql("now()")
                    .ValueGeneratedOnAdd();

                modelBuilder.Property(nameof(IEntity.UpdatedAt)).ValueGeneratedOnAddOrUpdate();
            }

            return builder;
        }
    }
}
