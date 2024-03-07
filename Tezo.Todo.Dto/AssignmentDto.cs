using Tezo.Todo.Models;

namespace Tezo.Todo.Dtos
{
    public class AssignmentDto
    {
        // View Models (DTOs) are used to prepare and manage data for display in the user interface.
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public Guid UserId { get; set; }
    }
}
