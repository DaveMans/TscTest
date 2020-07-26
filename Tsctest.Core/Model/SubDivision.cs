using System.ComponentModel.DataAnnotations.Schema;

namespace Tsctest.Core.Models
{
    public class SubDivision
    {
        public int SubDivisionId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
