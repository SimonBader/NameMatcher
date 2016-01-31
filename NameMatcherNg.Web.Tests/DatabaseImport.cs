using EntityFramework.BulkInsert.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameMatcherNg.Web.Models;
using NameMatcherNg.Web.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameMatcherNg.Web.Tests
{
    [TestClass]
    public class DatabaseImport
    {
        [TestMethod]
        public void ImportCountriesAndNamesIntoDB()
        {
            var countryService = new CountryService();
            var nameService = new NameService();

            Trace.WriteLine("Create DB if not exists");
            System.Data.Entity.Database.SetInitializer(new DBInitializer());

            Trace.WriteLine("Start DB sync");

            List<Country> countries = countryService.GetCountriesAsync().Result;

            using (var countryContext = new DBContext())
            {
                countryContext.BulkInsert(countries);
                countryContext.SaveChanges();

                int totalCountries = countries.Count();
                int countriesCount = 0;
                Trace.WriteLine($"DB sync: {totalCountries} countries downloaded");

                foreach (Country country in countryContext.Countries)
                {
                    Trace.WriteLine($"DB sync: start country {country.Name} ({countriesCount++} of {totalCountries})");
                    BabyName[] names = nameService.GetNamesAsync(country.HRef).Result.ToArray();

                    foreach (BabyName name in names)
                    {
                        name.CountryId = country.Id;
                    }

                    using (var nameContext = new DBContext())
                    {
                        nameContext.BulkInsert(names);
                        nameContext.SaveChanges();
                    }

                    Trace.WriteLine($"DB sync: end country {country.Name}: {names.Count()} names downloaded");
                }
            }

            Trace.WriteLine("End DB sync");
        }

        [TestMethod]
        public void ImportCountryCodesIntoDB()
        {
            var countryCodeService = new CountryCodeService();
            IDictionary<string, string> countryCodes = countryCodeService.GetCountryCodesAsync().Result;
            using (var db = new DBContext())
            {
                foreach(Country country in db.Countries)
                {
                    string countryCode = countryCodes.Single(x => x.Key == country.Name).Value;
                    country.CountryCode = countryCode;
                }

                db.SaveChanges();
            }
        }
    }
}
