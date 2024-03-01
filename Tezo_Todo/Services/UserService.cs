//using Tezo_Todo.Data;
//using Tezo_Todo.Models;
//using Tezo_Todo.Services.Interfaces;

//namespace Tezo_Todo.Services
//{
//    public class UserService : IUserServices
//    {
//        private readonly TodoAPIDbContext _dbContext;

//        public UserService(TodoAPIDbContext dbContext)
//        {
//            this._dbContext = dbContext;
//        }

//        public List<User> GetAllUser()
//        {
//            return _dbContext.User.ToList();
//        }

//        public User AddUser(User user)
//        {
//            _dbContext.User.AddAsync(user);
//            _dbContext.SaveChangesAsync();

//            return user;
//        }

//        public async Task<User> GetUserById(Guid id)
//        {
//            var user = await _dbContext.User.FindAsync(id);
//            return user;
//        }


//        public async Task<bool> UpdateUser(Guid id, User user)
//        {
//            try
//            {
//                var userData = _dbContext.User.Update(user);
//                _dbContext.SaveChanges();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }

//        }


//        public async Task<User> DeleteUser(Guid id)
//        {
//            var user = await GetUserById(id);
//            if (user != null)
//            {
//                _dbContext.User.Remove(user);
//                _dbContext.SaveChangesAsync();
//            }
//            return user;
//        }


//    }
//}
