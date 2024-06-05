using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    internal class ProductsRepository(ShopDbContext dbContext) : IProductsRepository
    {
        public async Task<int> Create(Product entity)
        {
            dbContext.Products.Add(entity);
            await dbContext.SaveChangesAsync(); 
            return entity.Id;
        }

        public async Task Delete(Product entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await dbContext.Products.Include(p => p.ProductCategory).ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await dbContext.Products.Include(p => p.ProductCategory).FirstOrDefaultAsync(product => product.Id == id);
            return product;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
