using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.Orders.Commands.CreateOrder.Tests
{
    public class CreateOrderCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedOrderId()
        {
            // arrange
            var mapperMock = new Mock<IMapper>();

            var command = new CreateOrderCommand();
            var order = new Order()
            {
                ProductId = 1,
                ProductQuantity = 2,
            };
            var product = new Product()
            {
                Id = 1,
                Quantity = 100,
                Price = 24.99M,
            };
            mapperMock.Setup(m => m.Map<Order>(command)).Returns(order);   

            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(repo => repo.Create(It.IsAny<Order>()))
                .ReturnsAsync(1);

            var productsRepositoryMock = new Mock<IProductsRepository>();
            productsRepositoryMock.Setup(repo => repo.GetByIdAsync(order.ProductId))
                .ReturnsAsync(product);

            var commandHanlder = new CreateOrderCommandHandler
            (
                ordersRepositoryMock.Object, 
                productsRepositoryMock.Object, 
                mapperMock.Object
            );

            // act
            var result = await commandHanlder.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(1);
            ordersRepositoryMock.Verify(r => r.Create(order), Times.Once);
        }

        [Fact()]
        public async Task Handle_ForInvalidCommand_ShouldThrowProductUnavailableException()
        {
            // arrange
            var mapperMock = new Mock<IMapper>();

            var command = new CreateOrderCommand();
            var order = new Order()
            {
                ProductId = 1,
                ProductQuantity = 101, // quantity exceeds available product quantity
            };
            var product = new Product()
            {
                Id = 1,
                Quantity = 100,
                Price = 24.99M,
            };
            mapperMock.Setup(m => m.Map<Order>(command)).Returns(order);

            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            var productsRepositoryMock = new Mock<IProductsRepository>();
            productsRepositoryMock.Setup(repo => repo.GetByIdAsync(order.ProductId))
                .ReturnsAsync(product);

            var commandHandler = new CreateOrderCommandHandler(
                ordersRepositoryMock.Object,
                productsRepositoryMock.Object,
                mapperMock.Object
            );

            // act & assert
            await Xunit.Assert.ThrowsAsync<ProductUnavailableException>(() =>
                commandHandler.Handle(command, CancellationToken.None));
        }
    }
}