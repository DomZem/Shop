using AutoMapper;
using FluentAssertions;
using Shop.Application.Orders.Commands.CreateOrder;
using Shop.Domain.Entities;
using Xunit;

namespace Shop.Application.Orders.Dtos.Tests
{
    public class OrdersProfileTests
    {
        private IMapper _mapper;

        public OrdersProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrdersProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForOrderToOrderDto_MapsCorrectly()
        {
            // arrange
            var order = new Order()
            {
                Id = 1,
                ProductQuantity = 1,
                TotalPrice = 100,
            };

            // act
            var orderDto = _mapper.Map<OrderDto>(order);

            // assert
            orderDto.Should().NotBeNull();
            orderDto.Id.Should().Be(order.Id);
            orderDto.ProductQuantity.Should().Be(order.ProductQuantity);    
            orderDto.OrderedAt.Should().Be(order.OrderedAt);    
        }

        [Fact()]
        public void CreateMap_ForCreateOrderCommandToOrder_MapsCorrectly()
        {
            // arrange
            var createOrderCommand = new CreateOrderCommand()
            {
                Street = "Test Street",
                City = "Test City",
                PostalCode = "12345",
                Country = "Test Country",
                ProductQuantity = 1,
                ProductId = 1,
                OrderStatusId = 1,
                OrderedById = "test",
            };
            
            // act
            var order = _mapper.Map<Order>(createOrderCommand);
                
            // assert
            order.Should().NotBeNull();
            order.OrderAddress.Street.Should().Be(createOrderCommand.Street);   
        }
    }
}