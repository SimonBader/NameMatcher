using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameMatcherNg.Web.Services;
using System.Collections.Generic;
using NameMatcherNg.Web.Models;
using System.Linq;
using System.Diagnostics;

namespace NameMatcherNg.Web.Tests
{
    [TestClass]
    public class NameServiceTests
    {
        [TestMethod]
        public void GetNamesAsyncTest()
        {
            string hrefGB = @"http://www.thomas-bayer.com:80/restnames/namesincountry.groovy?country=Great+Britain";
            var sut = new NameService();

            List<BabyName> names = sut.GetNamesAsync(hrefGB).Result;

            Assert.IsTrue(names.Any());
        }
    }
}
