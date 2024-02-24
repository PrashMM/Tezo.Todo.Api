using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Tezo_Todo.Models
{
    public class User
    {

        [Key]
        public string Id { get; set; }
        public string UniqueName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }


        [ForeignKey("Assignment")]
        public string AssignmentId { get; set; }
        public virtual List<Assignment> Assignments { get; set; }

        public User()
        {

        }

        public User(string id, string uniqueName, string firstName, string lastName, string password, Assignment assignment)
        {
            Id = id;
            UniqueName = uniqueName;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Assignments = new List<Assignment> ();
        }
    }
}
