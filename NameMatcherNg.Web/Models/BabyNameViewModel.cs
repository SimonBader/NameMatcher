using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Models
{
    public class BabyNameViewModel
    {
        public BabyNameViewModel(BabyName babyName)
        {
            Name = babyName.Name;
        }
        public string Name { get; set; }
    }
}