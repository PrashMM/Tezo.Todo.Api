using Tezo.Todo.Dto;

namespace Tezo.Todo.Dtos
{
    public class UserAssignmentsDto
    {
        public UserDto user { get; set; }
        public virtual List<AssignmentDto> Assignments { get; set; }
    }
}
