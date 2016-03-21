using System.ComponentModel.DataAnnotations;

namespace NameMatcherNg.Web.Models
{
    public class BabyName2Country
    {
        [Key]
        public int Id { get; set; }
        public Country Country { get; set; }
        public BabyName BabyName { get; set; }
        public int Frequency { get; set; }
    }
}