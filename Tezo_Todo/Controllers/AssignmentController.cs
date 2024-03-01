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
        public async Task<IActionResult> AddAssignment([FromRoute] Guid id, AssignmentDtos newTask)
        {
            // id represents :- user id(it asks for userId to get to know under whom i suppose to add new assignment),
            // newTask will be the new assignment suppose to be insert inside Table.
            var newAssignment = _assignmentServie.AddAssignment(id, newTask);  
            return Ok(newAssignment);
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

            var deletedTask = await _assignmentServie.DeleteAssignment(id);
            return Ok($"{deletedTask.Title} got deleted");
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
            var sortedTasks = _assignmentServie.SortByDate();
            return Ok(sortedTasks);
        }

        [HttpGet]
        [Route("FilterByStatus")]
        public async Task<IActionResult> FilterByStatus([FromQuery] Status status)
        {
            var filteredTasks = _assignmentServie.FilterByStatus(status);
            return Ok(filteredTasks);
        }


        [HttpGet]
        [Route("FilterByPriority")]
        public async Task<IActionResult> FilterByPriority([FromQuery] Priority priority)
        {
            var filteredTasks = _assignmentServie.FilterByPriority(priority);
            return Ok(filteredTasks);
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

        [HttpGet]
        [Route("PaginatedAssignments")]
        public async Task<IActionResult> GetAssignmentsWithAssignments(int pageIndex = 1, int pageSize = 10)
        {
            var assignments = await _assignmentServie.GetPaginatedAssignments(pageIndex, pageSize);
            return Ok(assignments);
        }

    }
}