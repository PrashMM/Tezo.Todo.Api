using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tezo.Todo.Data;
using Tezo.Todo.Dto;
using Tezo.Todo.Models;
using Tezo.Todo.Repositories;
using Tezo.Todo.Repository.Interfaces;

namespace Tezo.Todo.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoAPIDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(TodoAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            if (_dbContext.User.Any())
            {
                var undeletedUsers = await UndeletedUsers();
                return _mapper.Map<List<UserDto>>(undeletedUsers);
            }
            else
            {
                return new List<UserDto>();
            }
        }

        public async Task<User> AddUser(UserDto user)
        {
            var newUser = _mapper.Map<User>(user);

            newUser.CreatedOn = Extension.GetCurrentDateTime();

            await _dbContext.User.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            return user;
        }


        public async Task<bool> UpdateUser(Guid id, UserDto user)
        {
            try
            {
                user.ModifiedOn = Extension.GetCurrentDateTime();
                var userr = _mapper.Map<User>(user);
                userr.Id = id;
                var userData = _dbContext.User.Update(userr);
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
            if (user != null && user.IsDeleted == false)
            {
                user.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<List<User>> UndeletedUsers()
        {
            var undundeletedUsers = await _dbContext.User.Where(a => !a.IsDeleted).ToListAsync();
            return undundeletedUsers;
        }

    }
}
