using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameMatcherNg.Web.Models;
using NameMatcherNg.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameMatcherNg.Web.Tests
{


    [TestClass]
    public class CountryCodeServiceTests
    {
        private List<string> _countries = new List<string>
        {
"Great Britain                                           ",
"Ireland                                                ",
"U.S.A.                                                ",
"Italy                                                ",
"Malta                                               ",
"Portugal                                           ",
"Spain                                             ",
"France                                           ",
"Belgium                                         ",
"Luxembourg                                     ",
"Netherlands                               ",
"East Frisia                                  ",
"Germany                                     ",
"Austria                                    ",
"Swiss                                     ",
"Iceland                                  ",
"Denmark                                 ",
"Norway                                 ",
"Sweden                                ",
"Finland                              ",
"Estonia                             ",
"Latvia                             ",
"Lithuania                         ",
"Poland                           ",
"Czech Republic                  ",
"Slovakia                       ",
"Hungary                       ",
"Romania                      ",
"Bulgaria                    ",
"Bosnia and Herzegovina     ",
"Croatia                   ",
"Kosovo                   ",
"Macedonia               ",
"Montenegro             ",
"Serbia                ",
"Slovenia             ",
"Albania             ",
"Greece             ",
"Russia            ",
"Belarus          ",
"Moldova         ",
"Ukraine        ",
"Armenia       ",
"Azerbaijan   ",
"Georgia     ",
"Kazakhstan/Uzbekistan,etc. ",
"Turkey    ",
"Arabia/Persia ",
"Israel  ",
"China  ",
"India/Sri Lanka ",
"Japan ",
"Korea ",
"Vietnam"
        };
        [TestMethod]
        public void GetCountryCodeAsyncTest()
        {
            var sut = new CountryCodeService();

            IDictionary<string, string> countryCodes = sut.GetCountryCodesAsync().Result;
            using (var file = new StreamWriter("countries.txt"))
            {
                foreach (string countryName in _countries)
                {
                    var countryCode = countryCodes.FirstOrDefault(x => countryName.Contains(x.Key));

                    if(countryCode.Key == null)
                    {
                        file.WriteLine($"NOT FOUND {countryName}");
                    }
                    else
                    {
                        file.WriteLine(countryCode.Value);
                    }
                }
            }
            

            Assert.IsTrue(countryCodes.Any());
        }
    }
}
