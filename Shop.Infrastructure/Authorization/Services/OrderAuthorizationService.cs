using Microsoft.Extensions.Logging;
using Shop.Application.User;
using Shop.Domain.Constants;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;

namespace Shop.Infrastructure.Authorization.Services
{
    public class OrderAuthorizationService(ILogger<OrderAuthorizationService> logger, IUserContext userContext) : IOrderAuthorizationService
    {
        public bool Authorize(Order order, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for order id: {OrderId}",
                user.Email,
                resourceOperation,
                order.Id);

            if((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Read) && 
                (user.Id == order.OrderedById) || user.IsInRole(UserRoles.Admin))
            {
                return true;
            }

            //logger.
            return false;
        }
    }
}
