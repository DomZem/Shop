using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.OrderStatuses.Commands.UpdateOrderStatus.Tests
{
    public class UpdateOrderStatusCommandHandlerTests
    {
        private readonly Mock<IOrderStatusesRepository> _orderStatusesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock; 

        private readonly UpdateOrderStatusCommandHandler _handler;

        public UpdateOrderStatusCommandHandlerTests()
        {
            _orderStatusesRepositoryMock = new Mock<IOrderStatusesRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateOrderStatusCommandHandler(_orderStatusesRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldUpdateOrderStatus()
        {
            // arrange
            var orderStatusId = 1;
            var command = new UpdateOrderStatusCommand()
            {
                Id = orderStatusId,
                Name = "Test order status"
            };

            var orderStatus = new OrderStatus()
            {
                Id = orderStatusId,
            };

            _orderStatusesRepositoryMock.Setup(o => o.GetByIdAsync(orderStatusId))
                .ReturnsAsync(orderStatus);

            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert
            _orderStatusesRepositoryMock.Verify(o => o.SaveChanges(), Times.Once);
            _mapperMock.Verify(m => m.Map(command, orderStatus), Times.Once);    
        }

        [Fact]
        public async Task Handle_WithNoExistingOrderStatus_ShouldThrowNotFoundException()
        {
            // arrange
            var orderStatusId = 2;
            var request = new UpdateOrderStatusCommand()
            {
                Id = orderStatusId,
            };

            _orderStatusesRepositoryMock.Setup(o => o.GetByIdAsync(orderStatusId))
                .ReturnsAsync((OrderStatus?)null);

            // act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Order status with id: {orderStatusId} doesn't exist");
        }
    }
}