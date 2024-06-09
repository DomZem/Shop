using AutoMapper;
using Shop.Application.OrderStatuses.Commands.CreateOrderStatus;
using Shop.Application.OrderStatuses.Commands.UpdateOrderStatus;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities;

namespace Shop.Application.OrderStatuses.Dtos
{
    public class OrderStatusesProfile : Profile
    {
        public OrderStatusesProfile()
        {
            CreateMap<OrderStatus, OrderStatusDto>();
            CreateMap<CreateOrderStatusCommand, OrderStatus>();
            CreateMap<UpdateOrderStatusCommand, OrderStatus>();
            CreateMap<OrderStatusDto, OrderStatus>();
        }
    }
}
