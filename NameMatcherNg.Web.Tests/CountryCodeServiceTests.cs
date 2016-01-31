using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameMatcherNg.Web.Models;
using NameMatcherNg.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameMatcherNg.Web.Tests
{
    [TestClass]
    public class CountryCodeServiceTests
    {
        [TestMethod]
        public void GetCountryCodeAsyncTest()
        {
            var sut = new CountryCodeService();

            IDictionary<string, string> countryCodes = sut.GetCountryCodesAsync().Result;

            Assert.IsTrue(countryCodes.Any());
        }
    }
}
