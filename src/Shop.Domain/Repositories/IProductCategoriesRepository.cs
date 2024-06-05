using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IProductCategoriesRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();

        Task<ProductCategory?> GetByIdAsync(int id);

        Task<int> Create(ProductCategory entity);

        Task SaveChanges();
    }
}
