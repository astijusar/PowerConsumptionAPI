using Microsoft.EntityFrameworkCore;

namespace PowerConsumptionAPI.Models
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<PowerConsumption> PowerConsumptions { get; set; }

        internal Task FindAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
