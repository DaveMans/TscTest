using Tsctest.Core.Models;
using Tsctest.Dal;

namespace Tsctest.Tests.Helpers
{
    public static class DbContextExtensions
    {
         public static void Seed(this ApplicationDbContext dbContext)
        {

            dbContext.Countries.Add(new Country
            {
                CountryId = 1,
                Name = "Guatemala",
                AlphaCode2 = "GT",
                AlphaCode3 = "GTM",
                NumericCode = "320",
                Independent = true
            });

            dbContext.SaveChanges();
        }
    }
}
