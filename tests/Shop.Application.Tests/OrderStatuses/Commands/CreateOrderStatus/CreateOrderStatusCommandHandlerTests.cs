using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.OrderStatuses.Commands.CreateOrderStatus.Tests
{
    public class CreateOrderStatusCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ForValidCommand_ReturnsCreatedOrderStatusId()
        {
            // arrange
            var mapperMock = new Mock<IMapper>();

            var command = new CreateOrderStatusCommand();

            var orderStatus = new OrderStatus()
            {
                Name = "Shipped"
            };
            mapperMock.Setup(m => m.Map<OrderStatus>(command))
                .Returns(orderStatus);

            var orderStatusesRepositoryMock = new Mock<IOrderStatusesRepository>();
            orderStatusesRepositoryMock.Setup(repo => repo.Create(It.IsAny<OrderStatus>()))
                .ReturnsAsync(1);

            var commandHandler = new CreateOrderStatusCommandHandler(orderStatusesRepositoryMock.Object, mapperMock.Object);

            // act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(1);
            orderStatusesRepositoryMock.Verify(r => r.Create(orderStatus), Times.Once);
        }
    }
}