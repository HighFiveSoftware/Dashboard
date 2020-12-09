using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using Moq;
using Microsoft.Extensions.Logging;

namespace DashboardApi.Tests
{
    public class DashboardIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public DashboardIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("/")]
        public async Task GetEndpointsSuccess(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
                public void GetReturnsCorrectNumberOfElements()
                {
                   var logger = Mock.Of<ILogger<Controllers.DashboardController>>();
                    var controller = new Controllers.DashboardController(logger);
            var result = controller.Ok();
                    Assert.Equal("Wellcome1", result.ToString());
                 }
    }
}