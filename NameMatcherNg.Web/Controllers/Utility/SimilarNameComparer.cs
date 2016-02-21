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
            if (one == null)
            {
                return two == null;
            }
            if (two == null)
            {
                return one == null;
            }

            return one.Name == two.Name;
        }


        public int GetHashCode(BabyName babyName)
        {
            if(babyName == null)
            {
                return 0;
            }

            return babyName.Name.GetHashCode();
        }
    }
}