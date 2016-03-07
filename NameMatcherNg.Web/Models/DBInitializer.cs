using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Models
{
    //This function will ensure the database is created and seeded with any default data.
    public class DBInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        public DBInitializer()
        {

        }
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
        }
    }
}