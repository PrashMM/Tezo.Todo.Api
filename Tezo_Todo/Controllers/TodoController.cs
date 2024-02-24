using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tezo_Todo.Data;
using Tezo_Todo.Models;
using Tezo_Todo.Services;

namespace Tezo_Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoAPIDbContext dbContext;
        private AssignmentServie assignmentServie;
        public TodoController(TodoAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            assignmentServie = new AssignmentServie(dbContext);
        }


        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        {
            return Ok(await dbContext.Assignment.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignments(Assignment task)
        {
            var assignment = assignmentServie.AddAssignment(task);
            await dbContext.Assignment.AddAsync(assignment);
            await dbContext.SaveChangesAsync();
            return Ok(assignment);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateAssignment([FromRoute] Guid id, Assignment assignment)
        {
            var task = assignmentServie.UpdateAssignment(id, assignment);
            await dbContext.SaveChangesAsync();
            return Ok(task);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteAssignment([FromRoute] Guid id)
        {
            var task = await dbContext.Assignment.FindAsync(id);
            if (task != null)
            {
                dbContext.Assignment.Remove(task);
                await dbContext.SaveChangesAsync();
                return Ok($"{task.Title} got deleted");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchAssignment([FromQuery] string searchTerm)
        {
            var tasks = assignmentServie.SearchTask(searchTerm);

            foreach (var task in tasks)
            {
                dbContext.Assignment.AddAsync(task);
                dbContext.SaveChangesAsync();
            }
            return Ok(tasks);
        }

        [HttpGet]
        [Route("sort")]
        public async Task<IActionResult> SortAssignment()
        {
            var tasks = assignmentServie.SortByDate();
            foreach (var task in tasks)
            {
                dbContext.Assignment.AddAsync(task);
                dbContext.SaveChangesAsync();
            }
            return Ok(tasks);
        }

        [HttpGet]
        [Route("FilterStatus")]
        public async Task<IActionResult> FilterStatus([FromQuery] Status status)
        {
            var tasks = assignmentServie.FilterByStatus(status);

            foreach (var task in tasks)
            {
                dbContext.Assignment.AddAsync(task);
                dbContext.SaveChangesAsync();
            }
            return Ok(tasks);
        }


        [HttpGet]
        [Route("FilterPriority")]
        public async Task<IActionResult> FilterPriority([FromQuery] Priority priority)
        {
            var tasks = assignmentServie.FilterByPriority(priority);

            foreach (var task in tasks)
            {
                dbContext.Assignment.AddAsync(task);
                dbContext.SaveChangesAsync();
            }
            return Ok(tasks);
        }

    }
}