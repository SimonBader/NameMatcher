using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Models
{
    public class CountryViewModel
    {
        public CountryViewModel(Country country)
        {
            Name = country.Name;
            CountryCode = country.CountryCode;
        }
        public string Name { get; set; }
        public string CountryCode { get; set; }
    }
}