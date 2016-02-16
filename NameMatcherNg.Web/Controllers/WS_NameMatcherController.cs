using NameMatcherNg.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using System.Data.Entity;
using NameMatcherNg.Web.Controllers.Utility;

namespace NameMatcherNg.Web.Controllers
{
    public class WS_NameMatcherController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpPost]
        public async Task<List<BabyName>> Names(NamesBindingModel bindingModel)
        {
            var namesTotal = new List<BabyName>();

            foreach(string countryCode in bindingModel.CountryCodes)
            {
                Country country = db.Countries.Single(x => x.CountryCode == countryCode);
                List<BabyName> namesFromCountry = await db.Names.Where(x => x.CountryId == country.Id).ToListAsync();

                if(namesTotal.Any())
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

        [HttpPost]
        public async Task<List<Country>> Countries(CountriesBindingModel bindingModel)
        {
            if (bindingModel.Name == null)
            {
                return await db.Countries.ToListAsync();
            }
            else
            {
                var countryIdsWithSameName = db.Names.Where(x => x.Name == bindingModel.Name).Select(x => x.CountryId);
                return await db.Countries.Where(x => countryIdsWithSameName.Contains(x.Id)).ToListAsync();
            }
        }
    }
}
