using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class OrdersRepository(ShopDbContext dbContext) : IOrdersRepository
    {
        public async Task<int> Create(Order entity)
        {
            dbContext.Orders.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.Product)
                .ToListAsync();
            return orders;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            var order = await dbContext.Orders
                .Include (o => o.Product)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderedBy)
                .FirstOrDefaultAsync(order => order.Id == id);
            return order;
        }

        public async Task Delete(Order entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
