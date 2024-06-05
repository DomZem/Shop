using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByIdAsync(string id);

        Task<string> Create(User entity);

        Task Delete(User entity);

        Task SaveChanges();
    }
}
