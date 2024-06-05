using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Shop.API.Controllers.Tests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/products");

            // assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}