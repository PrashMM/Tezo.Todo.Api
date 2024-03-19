using System.Text;

using System.Security.Cryptography;
using Tezo.Todo.Models;
using Tezo.Todo.Repository.Interfaces;
using Tezo.Todo.Services.Interfaces;
using Tezo.Todo.Dto;

namespace Tezo.Todo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepo)
        {
            userRepository = userRepo;
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            return await userRepository.GetAllUser();
        }

        public async Task<User> AddUser(UserDto user)
        {
            string hashedPassword = HashPassword(user.Password);
            user.Password = hashedPassword;
            return await userRepository.AddUser(user);
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await userRepository.GetUserById(id);
        }


        public async Task<bool> UpdateUser(Guid id, UserDto user)
        {
            return await userRepository.UpdateUser(id, user);
        }


        public async Task<User> DeleteUser(Guid id)
        {
            return await userRepository.DeleteUser(id);
        }

        //to store password
        public static string HashPassword(string password)
        {
            using (var algorithm = new SHA256Managed())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = algorithm.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
