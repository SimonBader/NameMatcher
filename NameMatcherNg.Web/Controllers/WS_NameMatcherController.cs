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
        private DatabaseContext db = new DatabaseContext();

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

            return new List<BabyNameViewModel>();
        }

        [HttpPost]
        public async Task<List<CountryViewModel>> Countries(CountriesBindingModel bindingModel)
        {
            List<Country> countries;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (bindingModel.BabyNameFilter == null)
            {
                countries = await db.Countries.ToListAsync();
            }
            else
            {
                var babyName2Country = await db.BabyName2CountryList.Include("Country").Where(x => x.BabyName.Name == bindingModel.BabyNameFilter).ToListAsync();
                countries = babyName2Country.Select(x => x.Country).ToList();
            }

            stopwatch.Stop();
            Trace.WriteLine($"Duration Countries {stopwatch.ElapsedMilliseconds} ms");
            return countries.Select(x => new CountryViewModel(x)).ToList();
        }

        private async Task<List<BabyNameViewModel>> GetNamesByCountryCode(IList<string> countryCodes)
        {
            if (countryCodes.Count() == 0)
            {
                return new List<BabyNameViewModel>();
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var names = await db.Names.Where(x => x.BabyName2CountryList.Select(y => y.Country.CountryCode).Intersect(countryCodes).Count() == countryCodes.Count()).ToListAsync();
            var namesViewModel = names.Select(x => new BabyNameViewModel(x)).ToList();
            stopwatch.Stop();
            Trace.WriteLine($"Duration GetNamesByCountryCode: {stopwatch.ElapsedMilliseconds} ms");
            return namesViewModel;
        }

        private async Task<List<BabyNameViewModel>> GetNamesByFilter(string nameFilter)
        {
            if(nameFilter.Length < 2)
            {
                return new List<BabyNameViewModel>();
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var filteredNames = await db.Names.Where(x => x.Name.Contains(nameFilter)).Take(30).ToListAsync();
            var namesViewModel = filteredNames.Select(x => new BabyNameViewModel(x)).ToList();
            stopwatch.Stop();
            Trace.WriteLine($"Duration GetNamesByFilter: {stopwatch.ElapsedMilliseconds} ms");
            return namesViewModel;
        }
    }
}
