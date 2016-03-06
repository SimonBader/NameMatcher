using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        public Country()
        {
            Names = new HashSet<BabyName>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        [XmlIgnore]
        public virtual ICollection<BabyName> Names { get; set; }
    }

    public class State
    {
        [Key]
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Pin { get; set; }
        public double Offset { get; set; }
        public string Points { get; set; }
    }

    public class BabyName
    {
        public BabyName()
        {
            Countries = new HashSet<Country>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string line { get; set; }
        public bool IsFemale { get; set; }
        public int Frequency { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
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

        public DbSet<State> States { get; set; }

        public DbSet<BabyName> Names { get; set; }

    }

    //This function will ensure the database is created and seeded with any default data.
    public class DBInitializer : CreateDatabaseIfNotExists<DBContext>
    {
        public DBInitializer()
        {

        }
        protected override void Seed(DBContext context)
        {
            base.Seed(context);
        }
    }
}

