using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameMatcherNg.Web.Controllers;
using NameMatcherNg.Web.Models;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace NameMatcherNg.Web.Tests.Controllers
{
    [TestClass]
    public class WS_NameMatcherControllerTests
    {
        [TestMethod]
        public void SerializeTest()
        {
            var restnames = new Services.Dto.countriesReponse
            {
                countries = new Services.Dto.country[]
                {
                    new Services.Dto.country { value="Switzerland", href = "aa"},
                    new Services.Dto.country { value="Germany",href = "bb"},
                }
            };
            var serializer = new XmlSerializer(typeof(Services.Dto.countriesReponse));

            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, restnames);
                stream.Position = 0;
                string msg = new StreamReader(stream).ReadToEnd();
                Assert.IsFalse(string.IsNullOrWhiteSpace(msg));
            }
        }
    }
}
