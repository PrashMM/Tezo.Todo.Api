using Tezo.Todo.Dtos;
using Tezo.Todo.Models;

namespace Tezo.Todo.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUser();
        public Task<User> AddUser(User user);
        public Task<User> GetUserById(Guid id);
        public Task<bool> UpdateUser(Guid id, User user);
        public Task<User> DeleteUser(Guid id);
    }
}
