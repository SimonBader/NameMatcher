using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace NameMatcherNg.Web.Services.Dto
{
    public class name
    {
        [XmlIgnore]
        public int id;

        [XmlText]
        public string value;

        [XmlAttribute]
        public string href;
    }
}