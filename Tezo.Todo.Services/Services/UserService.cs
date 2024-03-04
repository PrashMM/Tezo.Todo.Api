using Tezo_Todo.Data;
using Tezo_Todo.Models;
using Tezo_Todo.Services.Interfaces;

namespace Tezo_Todo.Services
{
    public class UserService : IUserServices
    {     
        private readonly UserRepository userRepository;

        public UserService(TodoAPIDbContext dbContext)
        {
            userRepository = new UserRepository(dbContext);
        }

        public List<User> GetAllUser()
        {
            return userRepository.GetAllUser();
        }

        public User AddUser(User user)
        {
            return userRepository.AddUser(user);
        }

        public Task<User> GetUserById(Guid id)
        {
            return userRepository.GetUserById(id);
        }


        public  Task<bool> UpdateUser(Guid id, User user)
        {
            return userRepository.UpdateUser(id, user);
        }


        public  Task<User> DeleteUser(Guid id)
        {
            return userRepository.DeleteUser(id);
        }


    }
}
