using Microsoft.AspNetCore.Mvc;
using Tezo_Todo.Data;
using Tezo_Todo.Models;
using Tezo_Todo.Services;

namespace Tezo_Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(TodoAPIDbContext dbContext)
        {
            _userService = new UserService(dbContext);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(_userService.GetAllUser());
        }


        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(User user)
        {
            var newUser = _userService.AddUser(user);
            return Ok(newUser);
        }

        [HttpGet]
        [Route("GetUserById{id:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPut]
        [Route("Update{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, User user)
        {
            var updatedUser = _userService.UpdateUser(id, user);
            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route("Delete{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var deleteUser = await _userService.DeleteUser(id);
            return Ok($"{deleteUser.FirstName} got deleted");

        }
    }
}
