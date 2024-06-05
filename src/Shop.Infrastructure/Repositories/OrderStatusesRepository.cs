using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class OrderStatusesRepository(ShopDbContext dbContext) : IOrderStatusesRepository
    {
        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            var orderStatuses = await dbContext.OrderStatuses.OrderBy(os => os.Id).ToListAsync();
            return orderStatuses;
        }

        public async Task<OrderStatus?> GetByIdAsync(int id)
        {
            var orderStatus = await dbContext.OrderStatuses.FirstOrDefaultAsync(orderStatus => orderStatus.Id == id);
            return orderStatus;
        }

        public async Task<int> Create(OrderStatus entity)
        {
            dbContext.OrderStatuses.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
