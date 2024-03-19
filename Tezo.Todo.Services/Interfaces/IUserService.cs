using Tezo.Todo.Dto;
using Tezo.Todo.Models;

namespace Tezo.Todo.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUser();
        public Task<User> AddUser(UserDto user);
        public Task<User> GetUserById(Guid id);
        public Task<bool> UpdateUser(Guid id, UserDto user);
        public Task<User> DeleteUser(Guid id);
    }
}
