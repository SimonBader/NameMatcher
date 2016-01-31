using NameMatcherNg.Web.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net.Http.Headers;
using NameMatcherNg.Web.Services;
using NameMatcherNg.Web.Controllers.Utility;

namespace NameMatcherNg.Web.Controllers
{

    public class WS_NameMatcherController : ApiController
    {
        private DBContext db = new DBContext();

        [HttpPost]
        public async Task<List<BabyName>> Names(NameMatcherBindingModel bindingModel)
        {
            List<BabyName> namesCountryOne = await db.Names.Where(x => x.CountryId == bindingModel.CountryIdOne).ToListAsync();
            List<BabyName> namesCountryTwo = await db.Names.Where(x => x.CountryId == bindingModel.CountryIdTwo).ToListAsync();
            return namesCountryOne.Intersect(namesCountryTwo, new SimilarNameComparer()).ToList();
        }

        [HttpGet]
        public async Task<List<Country>> Countries()
        {
            return await db.Countries.ToListAsync();
        }
    }
}
