﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace NameMatcherNg.Web.Models
{
    public class Country
    {
        public Country()
        {
            Names = new HashSet<BabyName>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        [XmlIgnore]
        public virtual ICollection<BabyName> Names { get; set; }
    }
}