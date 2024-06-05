using AutoMapper;
using Shop.Application.Orders.Commands.CreateOrder;
using Shop.Application.Orders.Commands.UpdateOrder;
using Shop.Domain.Entities;

namespace Shop.Application.Orders.Dtos
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.OrderAddress.Street))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.OrderAddress.City))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.OrderAddress.PostalCode))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.OrderAddress.Country));

            CreateMap<CreateOrderCommand, Order>()
                .ForMember(d => d.OrderAddress, 
                    opt => opt.MapFrom(src => new OrderAddress
                    {
                        Street = src.Street,
                        City = src.City,
                        PostalCode = src.PostalCode,
                        Country = src.Country,
                    }));

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(d => d.OrderAddress,
                    opt => opt.MapFrom(src => new OrderAddress
                    {
                        Street = src.Street,
                        City = src.City,
                        PostalCode = src.PostalCode,
                        Country = src.Country,
                    }));
        }
    }
}
