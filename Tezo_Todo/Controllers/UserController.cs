using Microsoft.AspNetCore.Mvc;
using Tezo.Todo.Models;
using Tezo.Todo.Services.Interfaces;

namespace Tezo.Todo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _userService;
        public UserController(IUserServices userServices)
        {
            _userService = userServices;
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
