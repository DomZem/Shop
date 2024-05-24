using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);  
        
        Task<int> Create(Product entity);
    }
}
