using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace NameMatcherNg.Web.Services.Dto
{
    [XmlRoot(ElementName = "restnames")]
    public class countriesReponse
    {
        public country[] countries;
    }
}