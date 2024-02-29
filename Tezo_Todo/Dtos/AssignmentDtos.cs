using Tezo_Todo.Models;

namespace Tezo_Todo.Dtos
{
    public class AssignmentDtos
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public Guid UserId { get; set; }
    }
}
