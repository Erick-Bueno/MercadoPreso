using Microsoft.EntityFrameworkCore;

namespace SSO.Context;

public class SSODbContext : DbContext
{
    public SSODbContext(DbContextOptions options)
        : base(options) { }

    protected SSODbContext() { }
}
