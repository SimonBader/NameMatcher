namespace NameMatcherNg.Web.Models
{
    public class BabyNameViewModel
    {
        public BabyNameViewModel(BabyName babyName)
        {
            Name = babyName.Name;
            var count = babyName.CountriesWithSimilarNameCount;
            CountriesWithSimilarNameCount = count.HasValue ? count.Value : 0;
        }
        public string Name { get; set; }

        public int CountriesWithSimilarNameCount { get; set; }
    }
}