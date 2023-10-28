using Microsoft.EntityFrameworkCore;

namespace Cf.Infrastructure;

public class Context : DbContext
{
    static Context()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
    }

    public Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
