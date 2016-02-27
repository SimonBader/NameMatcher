using NameMatcherNg.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Data.Entity;
using NameMatcherNg.Web.Controllers.Utility;
using System;

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
        public async Task<List<BabyName>> Names(NamesBindingModel bindingModel)
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
        public async Task<List<Country>> Countries(CountriesBindingModel bindingModel)
        {
            if (bindingModel.BabyNameFilter == null)
            {
                return await db.Countries.ToListAsync();
            }
            else
            {
                var countryIdsWithSameName = db.Names.Where(x => x.Name == bindingModel.BabyNameFilter).Select(x => x.CountryId);
                return await db.Countries.Where(x => countryIdsWithSameName.Contains(x.Id)).ToListAsync();
            }
        }

        private async Task<List<BabyName>> GetNamesByCountryCode(IList<string> countryCodes)
        {
            var namesTotal = new List<BabyName>();

            foreach (string countryCode in countryCodes)
            {
                Country country = db.Countries.Single(x => x.CountryCode == countryCode);
                List<BabyName> namesFromCountry = await db.Names.Where(x => x.CountryId == country.Id).ToListAsync();

                if (namesTotal.Any())
                {
                    namesTotal = namesTotal.Intersect(namesFromCountry, new SimilarNameComparer()).ToList();
                }
                else
                {
                    namesTotal = namesFromCountry;
                }
            }

            return namesTotal.OrderBy(x => x.Name).ToList();
        }

        private async Task<List<BabyName>> GetNamesByFilter(string nameFilter)
        {
            if(nameFilter.Length < 2)
            {
                return new List<BabyName>();
            }

            List<BabyName> filteredNames = await db.Names.Where(x => x.Name.Contains(nameFilter)).Take(30).ToListAsync();
            var filteredDistinctNames = filteredNames.Distinct(new SimilarNameComparer());
            return filteredDistinctNames.ToList();
        }
    }
}
