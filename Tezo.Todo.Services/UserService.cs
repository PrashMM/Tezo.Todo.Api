using Tezo.Todo.Models;
using Tezo.Todo.Repository.Interfaces;
using Tezo.Todo.Services.Interfaces;

namespace Tezo.Todo.Services
{
    public class UserService : IUserServices
    {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepo)
        {
            userRepository = userRepo;
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


        public Task<bool> UpdateUser(Guid id, User user)
        {
            return userRepository.UpdateUser(id, user);
        }


        public Task<User> DeleteUser(Guid id)
        {
            return userRepository.DeleteUser(id);
        }


    }
}
