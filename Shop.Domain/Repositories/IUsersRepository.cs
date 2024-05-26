using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
