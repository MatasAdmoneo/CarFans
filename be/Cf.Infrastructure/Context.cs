using Cf.Domain.Aggregates.Adverts;
using Cf.Domain.Aggregates.Jobs;
using Cf.Domain.Aggregates.Reviews;
using Cf.Domain.Aggregates.Services;
using Microsoft.EntityFrameworkCore;

namespace Cf.Infrastructure;

public class Context : DbContext
{
    static Context()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
    }

    public Context() { }

    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Advert> Adverts { get; set; }

    public DbSet<Job> Jobs { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<WorkingDay> WorkingDays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Review)
            .WithOne(r => r.Job)
            .HasForeignKey<Review>(r => r.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Job>()
            .HasOne(j => j.Advert)
            .WithMany(a => a.Jobs)
            .HasForeignKey(j => j.AdvertId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkingDay>()
            .HasOne(w => w.Service)
            .WithMany(s => s.WeeklyWorkingHours)
            .HasForeignKey(w => w.ServiceId);

        modelBuilder.Entity<Service>()
            .HasIndex(s => s.ServiceId)
            .IsUnique();
    }
}
