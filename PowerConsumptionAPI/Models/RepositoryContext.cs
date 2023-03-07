using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;
using PowerConsumptionAPI.Models.Configuration;

namespace PowerConsumptionAPI.Models
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<PowerConsumption> PowerConsumptions { get; set; }
    }
}
