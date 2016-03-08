using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Models
{
    public class BabyName
    {
        public BabyName()
        {
            Countries = new HashSet<Country>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string line { get; set; }
        public bool IsFemale { get; set; }
        public int Frequency { get; set; }
        public int? CountriesWithSimilarNameCount { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}