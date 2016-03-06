using System.Collections.Generic;

namespace NameMatcherNg.Web.Models
{
    public class NamesBindingModel
    {
        public List<string> CountryCodes { get; set; }
        public string NameFilter { get; set; }
    }
}