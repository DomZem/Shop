using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
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
            var order = new Order();

            mapperMock.Setup(m => m.Map<Order>(command)).Returns(order);   

            var ordersRepositoryMock = new Mock<IOrdersRepository>();
            ordersRepositoryMock.Setup(repo => repo.Create(It.IsAny<Order>()))
                .ReturnsAsync(1);

            //var commandHanlder = new CreateOrderCommandHandler(ordersRepositoryMock.Object, mapperMock.Object);

            // act
            //var result = await commandHanlder.Handle(command, CancellationToken.None);

            // assert
            //result.Should().Be(1);
            ordersRepositoryMock.Verify(r => r.Create(order), Times.Once);
        }
    }
}