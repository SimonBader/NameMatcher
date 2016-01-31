using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Services.Dto
{
    public class RestResponse
    {
        public RestResponse()
        {
            messages = new List<string>();
            result = new List<countryCode>();
        }

        public List<string> messages { get; private set; }
        public List<countryCode> result { get; private set; }
    }
}