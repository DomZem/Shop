using Shop.Domain.Constants;
using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface IOrderAuthorizationService
    {
        bool Authorize(Order order, ResourceOperation resourceOperation);
    }
}
