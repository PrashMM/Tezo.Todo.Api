
namespace Tezo_Todo.Dtos
{
    public class UserDtos
    {
        public string UniqueName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual List<AssignmentDtos> Assignments { get; set; }
    }
}
