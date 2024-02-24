using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tezo_Todo.Data;
using Tezo_Todo.Models;

namespace Tezo_Todo.Services
{
    public class AssignmentServie
    {
        private TodoAPIDbContext dbContext;

        public AssignmentServie(TodoAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Assignment AddAssignment(Assignment task)
        {
            var assignment = new Assignment()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority
            };

            return assignment;
        }

        public async Task<Assignment> UpdateAssignment(Guid id, Assignment assignment)
        {
            var task = await dbContext.Assignment.FindAsync(id);

            task.Title = assignment.Title;
            task.Description = assignment.Description;
            task.DueDate = assignment.DueDate;
            task.Status = assignment.Status;
            task.Priority = assignment.Priority;

            return task;

        }

        public IEnumerable<Assignment> SearchTask(string searchTerm)
        {
            var tasks = dbContext.Assignment.Where(e => EF.Functions.Like(e.Title, $"%{searchTerm}%")).ToList();
            return tasks;
        }

        public IEnumerable<Assignment> SortByDate()
        {
            return dbContext.Assignment.OrderBy(e => e.DueDate);
        }

        public IEnumerable<Assignment> FilterByStatus(Status status)
        {
            return dbContext.Assignment.Where(e => e.Status == status);
        }

        public IEnumerable<Assignment> FilterByPriority(Priority priority)
        {
            return dbContext.Assignment.Where(e => e.Priority == priority);
        }
    }
}
