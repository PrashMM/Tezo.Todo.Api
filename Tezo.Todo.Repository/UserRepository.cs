using Microsoft.EntityFrameworkCore;
using Tezo.Todo.Data;
using Tezo.Todo.Models;
using Tezo.Todo.Repositories;
using Tezo.Todo.Repository.Interfaces;

namespace Tezo.Todo.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoAPIDbContext _dbContext;

        public UserRepository(TodoAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<User> AddUser(User user)
        {
            user.CreatedOn = Extension.GetCurrentDateTime();
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _dbContext.User.FindAsync(id);
            return user;
        }


        public async Task<bool> UpdateUser(Guid id, User user)
        {
            try
            {
                user.ModifiedOn = Extension.GetCurrentDateTime();
                var userData = _dbContext.User.Update(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<User> DeleteUser(Guid id)
        {
            var user = await GetUserById(id);
            if (user != null && user.IsDeleted == true)
            {
                user.IsDeleted = true;
                _dbContext.Entry(user).State = EntityState.Modified;
                //_dbContext.User.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }
    }
}
