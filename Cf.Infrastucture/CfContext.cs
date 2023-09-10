using Cf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cf.Infrastucture
{
    public class CfContext : DbContext
    {
        public CfContext() { }

        public CfContext(DbContextOptions<CfContext> options) : base(options) { }

        public DbSet<Client> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
