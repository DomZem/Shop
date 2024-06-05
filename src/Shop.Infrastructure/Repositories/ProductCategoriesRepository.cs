using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class ProductCategoriesRepository(ShopDbContext dbContext) : IProductCategoriesRepository
    {
        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            var productCategories = await dbContext.ProductCategories.OrderBy(pc => pc.Id).ToListAsync();
            return productCategories;
        }

        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            var productCategory = await dbContext.ProductCategories.FirstOrDefaultAsync(productCategory => productCategory.Id == id);
            return productCategory;
        }

        public async Task<int> Create(ProductCategory entity)
        {
            dbContext.ProductCategories.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
