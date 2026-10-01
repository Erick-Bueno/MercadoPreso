using Common.Infrastructure.TransactionOutbox;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Context;

public class CommonDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Outbox> Outbox { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("common");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommonDbContext).Assembly);
    }
}