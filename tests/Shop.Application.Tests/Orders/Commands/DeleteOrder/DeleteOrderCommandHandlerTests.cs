using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.Orders.Commands.DeleteOrder.Tests
{
    public class DeleteOrderCommandHandlerTests
    {
        private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
        private readonly DeleteOrderCommandHandler _handler;

        public DeleteOrderCommandHandlerTests()
        {
            _ordersRepositoryMock = new Mock<IOrdersRepository>();
            _handler = new DeleteOrderCommandHandler(_ordersRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldDeleteOrder()
        {
            // arrange
            var orderId = 1;
            var command = new DeleteOrderCommand()
            {
                Id = orderId,
            };

            var order = new Order()
            {
                Id = orderId
            };

            _ordersRepositoryMock.Setup(o => o.GetByIdAsync(orderId))
                .ReturnsAsync(order);
            _ordersRepositoryMock.Setup(o => o.Delete(It.IsAny<Order>()));

            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert
            _ordersRepositoryMock.Verify(o => o.Delete(order), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNoExistingOrder_ShouldThrowNotFoundException()
        {
            // arrange
            var orderId = 2;
            var request = new DeleteOrderCommand()
            {
                Id = orderId,
            };

            _ordersRepositoryMock.Setup(o => o.GetByIdAsync(orderId))
                .ReturnsAsync((Order?)null);

            // act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Order with id: {orderId} doesn't exist");
        }
    }
}