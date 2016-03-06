using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Models
{
    public class BabyNameViewModel
    {
        public BabyNameViewModel(BabyName babyName, int countriesWithSimilarNameCount)
        {
            Name = babyName.Name;
            CountriesWithSimilarNameCount = countriesWithSimilarNameCount;
        }
        public string Name { get; set; }

        public int CountriesWithSimilarNameCount { get; set; }
    }
}