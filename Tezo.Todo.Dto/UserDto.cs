namespace Tezo.Todo.Dtos
{
    public class UserDto
    {
        // View Models (DTOs) are used to prepare and manage data for display in the user interface.
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual List<AssignmentDto> Assignments { get; set; }
    }
}
