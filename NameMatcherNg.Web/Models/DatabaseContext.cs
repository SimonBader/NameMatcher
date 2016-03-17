using System.Data.Entity;

namespace NameMatcherNg.Web.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("applicationDB")
        {

        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<BabyName> Names { get; set; }
    }
}