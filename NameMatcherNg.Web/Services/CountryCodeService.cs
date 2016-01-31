using NameMatcherNg.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NameMatcherNg.Web.Services
{
    public class CountryCodeService
    {
        /// <summary>
        /// Returns a dictinary with key = [country name in english] value = [country code]
        /// </summary>
        public async Task<IDictionary<string, string>> GetCountryCodesAsync()
        {
            var countryCodes = new Dictionary<string, string>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://services.groupkt.com");

                HttpResponseMessage response = await client.GetAsync("/country/get/all");
                response.EnsureSuccessStatusCode();
                var restResponse = await response.Content.ReadAsAsync<Dto.CountryCodeResponse>();
                restResponse.RestResponse.result.ForEach(x => countryCodes.Add(x.name, x.alpha2_code));
            }

            return countryCodes;
        }
    }
}