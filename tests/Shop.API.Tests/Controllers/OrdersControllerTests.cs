using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Shop.API.Tests;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Seeders;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Shop.API.Controllers.Tests
{
    public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private readonly Mock<IOrdersRepository> _ordersRepositoryMock = new();

        private readonly Mock<IShopSeeder> _shopSeederMock = new();

        public OrdersControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    services.Replace(ServiceDescriptor.Scoped(typeof(IOrdersRepository),
                                                _ => _ordersRepositoryMock.Object));

                    services.Replace(ServiceDescriptor.Scoped(typeof(IShopSeeder),
                                                _ => _shopSeederMock.Object));
                });
            });
        }

        [Fact]
        public async Task GetAll_ForValidRequest_ShouldReturn200Ok()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/orders");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_ForInvalidRequest_ShouldReturn404NotFound()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/order");

            // assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForNonExisting_ShouldResturn404NotFound()
        {
            // arrange
            var id = 1234;

            _ordersRepositoryMock.Setup(m =>  m.GetByIdAsync(id)).ReturnsAsync((Order?)null);
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync($"/api/orders/{id}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForExisting_ShouldResturn200Ok()
        {
            // arrange
            var id = 99;

            var order = new Order()
            {
                Id = id,
                TotalPrice = 12.99M,
            };

            _ordersRepositoryMock.Setup(m => m.GetByIdAsync(id)).ReturnsAsync(order);
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync($"/api/orders/{id}");
            var orderDto = await response.Content.ReadFromJsonAsync<OrderDetailsDto>();

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            orderDto.Should().NotBeNull();
            orderDto.TotalPrice.Should().Be(12.99M);
        }
    }
}