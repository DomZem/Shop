using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<int> Create(Order entity);

        Task Delete(Order entity);

        Task SaveChanges();
    }
}
