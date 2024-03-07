using Tezo.Todo.Data;
using Tezo.Todo.Models;
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

        public List<User> GetAllUser()
        {
            return _dbContext.User.ToList();
        }

        public User AddUser(User user)
        {
            _dbContext.User.AddAsync(user);
            _dbContext.SaveChangesAsync();

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
                var userData = _dbContext.User.Update(user);
                _dbContext.SaveChanges();
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
            if (user != null)
            {
                _dbContext.User.Remove(user);
                _dbContext.SaveChangesAsync();
            }
            return user;
        }


    }
}
