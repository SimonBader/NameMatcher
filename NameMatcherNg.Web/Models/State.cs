using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NameMatcherNg.Web.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Pin { get; set; }
        public double Offset { get; set; }
        public string Points { get; set; }
    }
}