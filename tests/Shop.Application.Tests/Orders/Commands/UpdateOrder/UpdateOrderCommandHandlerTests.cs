using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.Orders.Commands.UpdateOrder.Tests
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly UpdateOrderCommandHandler _handler;

        public UpdateOrderCommandHandlerTests()
        {
            _ordersRepositoryMock = new Mock<IOrdersRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateOrderCommandHandler(_ordersRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateOrder()
        {
            // arrange
            var orderId = 1;
            var command = new UpdateOrderCommand()
            {
                Id = orderId,
                Street = "",
                City = "",
                PostalCode = "",
                Country = "",
                ProductQuantity = 1,
                ProductId = 1,
                OrderStatusId = 1,
                OrderedById = "test"
            };

            var order = new Order()
            {
                Id = orderId,
            };

            _ordersRepositoryMock.Setup(o => o.GetByIdAsync(orderId)).ReturnsAsync(order);

            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert
            _ordersRepositoryMock.Verify(o => o.SaveChanges(), Times.Once);
            _mapperMock.Verify(m => m.Map(command, order), Times.Once);
        }

        [Fact()]
        public async Task Handle_WithNoExistingOrder_ShouldThrowNotFoundException()
        {
            // arrange
            var orderId = 2;
            var request = new UpdateOrderCommand()
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