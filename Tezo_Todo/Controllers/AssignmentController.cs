using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tezo_Todo.Data;
using Tezo_Todo.Dtos;
using Tezo_Todo.Models;
using Tezo_Todo.Services;

namespace Tezo_Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentServie _assignmentServie;

        public AssignmentController(TodoAPIDbContext dbContext, IMapper mapper)
        {
            _assignmentServie = new AssignmentServie(dbContext, mapper);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAssignments()
        {
            return Ok(_assignmentServie.GetAllAssignments());
        }


        [HttpPost]
        [Route("Add{id:Guid}")]
        public async Task<IActionResult> AddAssignment([FromRoute] Guid id, AssignmentDtos task)
        {
            var assignment = _assignmentServie.AddAssignment(id, task);   // id represents - user id
            return Ok(assignment);
        }

        [HttpPut]
        [Route("Update{id:Guid}")]
        public async Task<IActionResult> UpdateAssignment([FromRoute] Guid id, AssignmentDtos assignment)
        {
            await _assignmentServie.UpdateAssignment(id, assignment);
            return Ok("success");
        }

        [HttpDelete]
        [Route("Delete{id:Guid}")]
        public async Task<IActionResult> DeleteAssignment([FromRoute] Guid id)
        {

            var task = await _assignmentServie.DeleteAssignment(id);
            return Ok($"{task.Title} got deleted");
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> SearchAssignment([FromQuery] string searchTerm)
        {
            var tasks = _assignmentServie.SearchTask(searchTerm);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("SortByDate")]
        public async Task<IActionResult> SortAssignments()
        {
            var tasks = _assignmentServie.SortByDate();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("FilterByStatus")]
        public async Task<IActionResult> FilterByStatus([FromQuery] Status status)
        {
            var tasks = _assignmentServie.FilterByStatus(status);
            return Ok(tasks);
        }


        [HttpGet]
        [Route("FilterByPriority")]
        public async Task<IActionResult> FilterByPriority([FromQuery] Priority priority)
        {
            var tasks = _assignmentServie.FilterByPriority(priority);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("RespectiveUser&Tasks{id:Guid}")]
        public async Task<IActionResult> ShowRespectiveUserAssignments(Guid id)
        {
            var assignment = _assignmentServie.GetUserRespectiveAssignments(id);
            return Ok(assignment);
        }


        [HttpGet]
        [Route("AllUsersTasks")]
        public async Task<IActionResult> ShowAllUsersAllAssignments()
        {
            var assignment = _assignmentServie.GetAllUserAllAssignments();
            return Ok(assignment);
        }

    }
}