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
        public async Task<List<BabyName>> Names(NameMatcherBindingModel bindingModel)
        {
            Country countryOne = db.Countries.Single(x => x.CountryCode == bindingModel.CountryCodeOne);
            Country countryTwo = db.Countries.Single(x => x.CountryCode == bindingModel.CountryCodeTwo);
            List<BabyName> namesCountryOne = await db.Names.Where(x => x.CountryId == countryOne.Id).ToListAsync();
            List<BabyName> namesCountryTwo = await db.Names.Where(x => x.CountryId == countryTwo.Id).ToListAsync();
            return namesCountryOne.Intersect(namesCountryTwo, new SimilarNameComparer()).ToList();
        }

        [HttpGet]
        public async Task<List<Country>> Countries()
        {
            return await db.Countries.ToListAsync();
        }
    }
}
