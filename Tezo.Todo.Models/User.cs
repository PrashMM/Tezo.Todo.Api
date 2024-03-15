using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tezo.Todo.Models
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }   // HashPassword, storing in services file
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

        public User()
        {

        }

        public User(Guid id, string userName, string firstName, string lastName, string password)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
    }
}
