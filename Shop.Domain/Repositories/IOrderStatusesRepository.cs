using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IOrderStatusesRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllAsync();

        Task<OrderStatus?> GetByIdAsync(int id);

        Task<int> Create(OrderStatus entity);

        Task SaveChanges();
    }
}
