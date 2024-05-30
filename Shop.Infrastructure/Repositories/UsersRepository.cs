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

        public async Task<User?> GetByIdAsync(string id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user;
        }

        public async Task<string> Create(User entity)
        {
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(User entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        public Task SaveChanges() => dbContext.SaveChangesAsync();
    }
}
