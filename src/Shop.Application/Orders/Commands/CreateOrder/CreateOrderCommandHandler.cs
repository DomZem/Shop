using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IOrdersRepository ordersRepository, IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<CreateOrderCommand, int>
    {
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<Order>(request);
                
            // find product with id
            var product = await productsRepository.GetByIdAsync(order.ProductId);
                
            if(product.Quantity < order.ProductQuantity)
            {
                throw new ProductUnavailableException();
            }

            order.TotalPrice = order.ProductQuantity * product.Price;
            product.Quantity -= order.ProductQuantity;
            await productsRepository.SaveChanges();
            // save changes
            int id = await ordersRepository.Create(order);
            return id;
        }
    }
}
