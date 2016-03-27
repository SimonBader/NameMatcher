namespace NameMatcherNg.Web.Models
{
    public class BabyNameViewModel
    {
        public BabyNameViewModel(BabyName babyName)
        {
            Name = babyName.Name;
            IsFemale = babyName.IsFemale;
            var count = babyName.CountriesWithSimilarNameCount;
            CountriesWithSimilarNameCount = count.HasValue ? count.Value : 0;
        }
        public string Name { get; set; }

        public bool IsFemale { get; set; }

        public int CountriesWithSimilarNameCount { get; set; }
    }
}