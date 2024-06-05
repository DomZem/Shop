using AutoMapper;
using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.OrderStatuses.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler(IOrderStatusesRepository orderStatusesRepository, IMapper mapper) : IRequestHandler<UpdateOrderStatusCommand>
    {
        public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var orderStatus = await orderStatusesRepository.GetByIdAsync(request.Id);
            if (orderStatus is null)
            {
                throw new NotFoundException("Order status", request.Id.ToString());
            }
            orderStatus.Name = request.Name;
            //mapper.Map(request, orderStatus);
            await orderStatusesRepository.SaveChanges();
        }
    }
}
