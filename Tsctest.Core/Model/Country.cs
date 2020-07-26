using System.Collections.Generic;

namespace Tsctest.Core.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string AlphaCode2 { get; set; }
        public string AlphaCode3 { get; set; }
        public string NumericCode { get; set; }
        public bool Independent { get; set; }
        public IEnumerable<SubDivision> SubDivisions { get; set; }
    }
}