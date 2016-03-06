using NameMatcherNg.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameMatcherNg.Web.Tests
{
    public class CtGenderWrapper
    {
        private Dictionary<string, int> _countries = new Dictionary<string, int>
        {
            {"GB", 31} ,
            {"IE", 32},
            {"US", 33},
            {"IT", 34},
            {"MT", 35},
            {"PT", 36},
            {"ES", 37},
            {"FR", 38},
            {"BE", 39},
            {"LU", 40},
            {"NL", 41},
            {"FY", 42},
            {"DE", 43},
            {"AT", 44},
            {"CH", 45},
            {"IS", 46},
            {"DK", 47},
            {"NO", 48},
            {"SE", 49},
            {"FI", 50},
            {"EE", 51},
            {"LV", 52},
            {"LT", 53},
            {"PL", 54},
            {"CZ", 55},
            {"SK", 56},
            {"HU", 57},
            {"RO", 58},
            {"BG", 59},
            {"BA", 60},
            {"HR", 61},
            {"KV", 62},
            {"MK", 63},
            {"ME", 64},
            {"RS", 65},
            {"SI", 66},
            {"AL", 67},
            {"GR", 68},
            {"RU", 69},
            {"BY", 70},
            {"MD", 71},
            {"UA", 72},
            {"AM", 73},
            {"AZ", 74},
            {"GE", 75},
            {"KZ", 76},
            {"TR", 77},
            {"FA", 78},
            {"IL", 79},
            {"CN", 80},
            {"IN", 81},
            {"JP", 82},
            {"KR", 83},
            { "VN", 84}
        };

        const int genderIndex = 0;
        const int nameIndex = 3;
        const int sortIndex = 30;

        public IDictionary<string, int> GetCountryCodesWithFrequency(string countriesField)
        {
            var countryCodesWithFrequency = new Dictionary<string, int>();

            foreach (var country in _countries)
            {
                var frequency = countriesField[country.Value - 1];

                if (frequency != ' ')
                {
                    countryCodesWithFrequency.Add(country.Key, Convert.ToInt32(frequency.ToString(), 16));
                }
            }

            return countryCodesWithFrequency;
        }

        public IList<BabyName> GetNames()
        {
            var names = new List<BabyName>();

            using (var file = new StreamReader("nam_dict.txt", Encoding.GetEncoding("ISO-8859-1")))
            {
                string line;
                var babyNames = new List<BabyName>();

                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("#") || line.StartsWith("="))
                    {
                        continue;
                    }

                    string genderField = line.Substring(genderIndex, nameIndex - genderIndex).Trim();
                    string nameField = line.Substring(nameIndex, sortIndex - nameIndex).Trim();

                    foreach (var gender in GetGenders(genderField))
                    {
                        foreach (var name in GetNames(nameField))
                        {
                            names.Add(new BabyName
                            {
                                Name = name,
                                line = line
                            });
                        }
                    }        
                }
            }

            return names;
        }

        private IList<string> GetNames(string nameField)
        {
            var names = new List<string>();


            if (nameField.Contains("+"))
            {
                names.Add(nameField.Replace("+", " "));
                names.Add(nameField.Replace("+", "-"));
                names.Add(nameField.Replace("+", string.Empty));
            }
            else
            {
                names.Add(nameField);
            }

            return names;
        }

        /// <summary>
        /// IsFemale = true: female
        /// IsFemale = false: male
        /// </summary>
        private IList<bool> GetGenders(string genderField)
        {
            var genders = new List<bool>();
            var females = new List<string> { "M", "1M", "?M", "?" };
            var males = new List<string> { "F", "1F", "?F", "?" };

            if (females.Contains(genderField))
            {
                genders.Add(true);
            }
            if (males.Contains(genderField))
            {
                genders.Add(true);
            }

            return genders;
        }
    }
}
