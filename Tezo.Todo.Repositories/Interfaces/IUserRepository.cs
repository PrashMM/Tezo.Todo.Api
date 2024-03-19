using Tezo.Todo.Dto;
using Tezo.Todo.Models;

namespace Tezo.Todo.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserDto>> GetAllUser();
        public Task<User> AddUser(UserDto user);
        public Task<User> GetUserById(Guid id);
        public Task<bool> UpdateUser(Guid id, UserDto user);
        public Task<User> DeleteUser(Guid id);
    }
}
