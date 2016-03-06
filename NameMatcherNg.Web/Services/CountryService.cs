using NameMatcherNg.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace NameMatcherNg.Web.Services
{
    public class CountryService
    {
        public async Task<List<Country>> GetCountriesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.thomas-bayer.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpResponseMessage response = await client.GetAsync("/restnames/countries.groovy");
                response.EnsureSuccessStatusCode();

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    var serializer = new XmlSerializer(typeof(Services.Dto.countriesReponse));
                    Dto.countriesReponse countriesResponse = (Dto.countriesReponse)serializer.Deserialize(stream);

                    return GetCountries(countriesResponse);
                }
            }
        }

        private List<Country> GetCountries(Dto.countriesReponse response)
        {
            var countries = new List<Country>();

            foreach(var country in response.countries)
            {
                countries.Add(new Country
                {
                    Name = country.value
                });
            }

            return countries;
        }
    }
}