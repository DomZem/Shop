using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class UsersRepository(ShopDbContext dbContext) : IUsersRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await dbContext.Users.ToListAsync();
            return users;
        }
    }
}
