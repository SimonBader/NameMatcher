using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NameMatcherNg.Web.Models
{
    public class BabyName2Country
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int BabyNameId { get; set; }
        public BabyName BabyName { get; set; }
        public int Frequency { get; set; }
    }
}