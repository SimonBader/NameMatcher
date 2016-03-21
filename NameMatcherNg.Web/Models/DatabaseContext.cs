using System.Data.Entity;

namespace NameMatcherNg.Web.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("applicationDB")
        {

        }

        public DbSet<BabyName> Names { get; set; }

        public DbSet<BabyName2Country> BabyName2CountryList { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }
    }
}