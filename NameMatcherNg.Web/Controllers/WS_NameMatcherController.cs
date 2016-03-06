﻿using NameMatcherNg.Web.Models;
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
                BabyName name = await db.Names.FirstAsync(x => x.Name == bindingModel.BabyNameFilter); // TODO: currently there are some name duplicates in DB.
                countries = name.Countries.ToList();
            }

            return countries.Select(x => new CountryViewModel(x)).ToList();
        }

        private async Task<List<BabyNameViewModel>> GetNamesByCountryCode(IList<string> countryCodes)
        {
            var names = await db.Names.Include("Countries").Where(x => x.Countries.Select(y => y.CountryCode).Intersect(countryCodes).Count() == countryCodes.Count()).ToListAsync();
            return names.Select(x => new BabyNameViewModel(x, x.Countries.Count())).ToList();
        }

        private async Task<List<BabyNameViewModel>> GetNamesByFilter(string nameFilter)
        {
            if(nameFilter.Length < 2)
            {
                return new List<BabyNameViewModel>();
            }

            var filteredNames = await db.Names.Include("Countries").Where(x => x.Name.Contains(nameFilter)).Take(30).ToListAsync();
            return filteredNames.Select(x => new BabyNameViewModel(x, x.Countries.Count())).ToList();
        }
    }
}
