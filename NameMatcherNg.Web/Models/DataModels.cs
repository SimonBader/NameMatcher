using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NameMatcherNg.Web.Models
{
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string firstName { get; set; }

        public virtual List<todoItem> todoItems { get; set; }
    }

    public class todoItem
    {
        [Key]
        public int id { get; set; }
        public string task { get; set; }
        public bool completed { get; set; }
    }

    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string HRef { get; set; }
        public string CountryCode { get; set; }

    }

    public class BabyName
    {
        [Key]
        public int Id { get; set; }

        public int CountryId { get; set; }

        public string Name { get; set; }

        public string HRef { get; set; }
    }

    public class DBContext : IdentityDbContext<User>
    {
        public DBContext() : base("applicationDB")
        {

        }
        //Override default table names
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static DBContext Create()
        {
            return new DBContext();
        }

        public DbSet<todoItem> todos { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<BabyName> Names { get; set; }

    }

    //This function will ensure the database is created and seeded with any default data.
    public class DBInitializer : CreateDatabaseIfNotExists<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            base.Seed(context);
        }
    }
}

