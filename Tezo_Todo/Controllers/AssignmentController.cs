using Microsoft.AspNetCore.Mvc;
using Tezo.Todo.Services.Interfaces;
using Tezo.Todo.Dtos;
using Tezo.Todo.Dto;
namespace Tezo.Todo.Api.Controllers
{
    [Route("api/[controller]")]   // Specifie the base route for the controller. [controller] is a token replaced with the name of the controller, which in this case is AssignmentController.
    [ApiController] // Indicates that the controller is an API controller. It enables automatic model validation, among other features.
    public class AssignmentController : ControllerBase //  ControllerBase handles content negotiation, allowing clients to request data in different formats (e.g., JSON, XML) based on the Accept header of the HTTP request.
    {
        private readonly IAssignmentService _assignmentServie;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentServie = assignmentService;
        }


        [HttpGet("GetAll")]  // Defines an HTTP GET endpoint with the route "api/Assignment/GetAll". This endpoint retrieves all assignments.
        public async Task<IActionResult> GetAllAssignments()
        {
            return Ok(await _assignmentServie.GetAllAssignments());
        }


        [HttpPost]
        [Route("{id:Guid}")]  // Defines an HTTP POST endpoint with the route "api/Assignment/Add{id:Guid}". [FromRoute] represents the user ID under whom the new assignment is to be added.
        public async Task<IActionResult> AddAssignment([FromRoute] Guid id, AssignmentDto newTask)
        {
            // id represents :- user id(it asks for userId to get to know under whom i suppose to add new assignment),
            // newTask will be the new assignment suppose to be insert inside Table.
            var newAssignment = await _assignmentServie.AddAssignment(id, newTask);
            return Ok(newAssignment);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAssignment([FromRoute] Guid id, AssignmentDto assignment)
        {
            await _assignmentServie.UpdateAssignment(id, assignment);
            return Ok("success");
        }
        // Task : The method is asynchronous(Task), allowing it to perform asynchronous operations without blocking the request thread.
        // IActionResult : The method will eventually return an HTTP response(IActionResult), representing the result of the action


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAssignment([FromRoute] Guid id)
        {
            var deletedTask = await _assignmentServie.DeleteAssignment(id);
            return Ok($"{deletedTask.Title} got deleted");
        }


        // searching, Sorting, filtering based on Status and priority.

        [HttpGet]
        [Route("FilterTasks")]
        public async Task<IActionResult> FilterAssignments(
    [FromQuery] string searchTerm,
    [FromQuery] bool isSort,
    [FromQuery] AssignmentFilter filter)
        {
            var filteredTasks = await _assignmentServie.FilterAssignments(searchTerm, isSort, filter); 
            return Ok(filteredTasks);
        }


        [HttpGet]
        [Route("RespectiveUser&Tasks{id:Guid}")]
        public async Task<IActionResult> ShowRespectiveUserAssignments(Guid id)
        {
            var assignment = await _assignmentServie.GetUserRespectiveAssignments(id);
            return Ok(assignment);
        }

        [HttpGet]
        [Route("AllUsersTasks")]
        public async Task<IActionResult> ShowUsersAssignments()
        {
            var assignment = await _assignmentServie.GetUsersAssignments();
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