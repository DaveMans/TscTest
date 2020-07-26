using Moq;
using Tsctest.Dal;
using Tsctest.WebApi.Controllers;

namespace Tsctest.Test.Controller
{
    public class CountryTestController
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly CountryController _controller;

        public CountryTestController()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _controller = new CountryController(_mockContext.Object);
        }


        [Fact]
        public void Index_ActionExecutes()
        {

        }
    }
}
