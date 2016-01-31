using NameMatcherNg.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace NameMatcherNg.Web.Services
{
    public class NameService
    {
        public async Task<List<BabyName>> GetNamesAsync(string href)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpResponseMessage response = await client.GetAsync(href);
                response.EnsureSuccessStatusCode();

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    var serializer = new XmlSerializer(typeof(Services.Dto.namesResponse));
                    Dto.namesResponse namesResponse = (Dto.namesResponse)serializer.Deserialize(stream);

                    return GetNames(namesResponse);
                }
            }
        }

        private List<BabyName> GetNames(Dto.namesResponse response)
        {
            var babyNames = new List<BabyName>();

            foreach(var name in response.names)
            {
                babyNames.Add(new BabyName
                {
                    Name = name.value,
                    HRef = name.href
                });
            }

            return babyNames;
        }
    }
}