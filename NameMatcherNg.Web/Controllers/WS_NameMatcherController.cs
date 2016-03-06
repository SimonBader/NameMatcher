using NameMatcherNg.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Data.Entity;
using NameMatcherNg.Web.Controllers.Utility;
using System;
using System.Diagnostics;

namespace NameMatcherNg.Web.Controllers
{
    public class WS_NameMatcherController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public async Task<List<State>> States()
        {
            return await db.States.ToListAsync();
        }

        [HttpPost]
        public async Task<List<BabyNameViewModel>> Names(NamesBindingModel bindingModel)
        {
            if(bindingModel.CountryCodes != null)
            {
                return await GetNamesByCountryCode(bindingModel.CountryCodes);
            }

            if(bindingModel.NameFilter != null)
            {
                return await GetNamesByFilter(bindingModel.NameFilter);
            }

            throw new NotSupportedException("Failed to get Names: whether a list of country codes nor a name filter have been passed.");
        }

        [HttpPost]
        public async Task<List<CountryViewModel>> Countries(CountriesBindingModel bindingModel)
        {
            List<Country> countries;
            if (bindingModel.BabyNameFilter == null)
            {
                countries = await db.Countries.ToListAsync();
            }
            else
            {
                BabyName name = await db.Names.SingleAsync(x => x.Name == bindingModel.BabyNameFilter);
                countries = name.Countries.ToList();
            }

            return countries.Select(x => new CountryViewModel(x)).ToList();
        }

        private async Task<List<BabyNameViewModel>> GetNamesByCountryCode(IList<string> countryCodes)
        {
            var namesTotal = new List<BabyName>();

            foreach (string countryCode in countryCodes)
            {
                Country country = await db.Countries.SingleOrDefaultAsync(x => x.CountryCode == countryCode);

                if (country == null)
                {
                    Trace.WriteLine($"The country with country code '{countryCode}' does not exist");
                    namesTotal.Clear();
                    break;
                }

                if (namesTotal.Any())
                {
                    namesTotal = namesTotal.Intersect(country.Names, new SimilarNameComparer()).ToList();
                }
                else
                {
                    namesTotal = country.Names.ToList();
                }
            }

            return namesTotal.Select(x => new BabyNameViewModel(x)).ToList();
        }

        private async Task<List<BabyNameViewModel>> GetNamesByFilter(string nameFilter)
        {
            if(nameFilter.Length < 2)
            {
                return new List<BabyNameViewModel>();
            }

            List<BabyName> filteredNames = await db.Names.Where(x => x.Name.Contains(nameFilter)).Take(30).ToListAsync();
            return filteredNames.Select(x => new BabyNameViewModel(x)).ToList(); ;
        }
    }
}
