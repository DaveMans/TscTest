using System.Threading.Tasks;
using Tsctest.Core.Models;
using Tsctest.Tests.Helpers;
using Tsctest.WebApi.Controllers;
using Xunit;

namespace Tsctest.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestGetCountrieById()
        {
            // Arrange
            var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestGetCountrieById));
            var controller = new CountryController(dbContext);

            // Act
            var dummyCountry = new Country
            {
                CountryId = 2,
                Name = "El Salvador",
                AlphaCode2 = "SL",
                AlphaCode3 = "SLV",
                NumericCode = "220",
                Independent = true
            };
            var response = await controller.CreateCountry(dummyCountry);
            var value = response.Value;

            dbContext.Dispose();

            // Assert
            Assert.True(value != null);

        }

    }
}
