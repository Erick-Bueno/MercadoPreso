using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Common.Infrastructure.Context;

public sealed class CommonDbContextFactory : IDesignTimeDbContextFactory<CommonDbContext>
{
    private const string ConnectionString =
        "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=mercado_preso;";

    public CommonDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<CommonDbContext>()
            .UseNpgsql(
                ConnectionString,
                o => o.MigrationsHistoryTable("__EFMigrationsHistory", "common")
            )
            .Options;

        return new CommonDbContext(options);
    }
}
