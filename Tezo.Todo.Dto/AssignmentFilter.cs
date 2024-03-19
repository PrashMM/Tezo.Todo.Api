using Tezo.Todo.Models;

namespace Tezo.Todo.Dto
{
    public class AssignmentFilter
    {
        public Status? status { get; set; }
        public Priority? priority { get; set; }

    }
}
