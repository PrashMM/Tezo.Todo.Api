using Tezo.Todo.Models;

namespace Tezo.Todo.Services.Interfaces
{
    public interface IUserServices
    {
        public List<User> GetAllUser();
        public User AddUser(User user);
        public Task<User> GetUserById(Guid id);
        public Task<bool> UpdateUser(Guid id, User user);
        public Task<User> DeleteUser(Guid id);
    }
}
