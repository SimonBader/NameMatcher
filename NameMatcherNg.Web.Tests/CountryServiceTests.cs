using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameMatcherNg.Web.Services;
using System.Collections.Generic;
using NameMatcherNg.Web.Models;
using System.Linq;

namespace NameMatcherNg.Web.Tests
{
    [TestClass]
    public class CountryServiceTests
    {
        [TestMethod]
        public void GetCountriesAsyncTest()
        {
            var sut = new CountryService();

            List<Country> countries = sut.GetCountriesAsync().Result;

            Assert.IsTrue(countries.Any());
        }
    }
}
