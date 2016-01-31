using NameMatcherNg.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Controllers.Utility
{
    class SimilarNameComparer : IEqualityComparer<BabyName>
    {

        public bool Equals(BabyName one, BabyName two)
        {
            return one.Name == two.Name;
        }


        public int GetHashCode(BabyName babyName)
        {
            return babyName.Name.GetHashCode();
        }
    }
}