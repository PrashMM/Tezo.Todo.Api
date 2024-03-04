using Tezo_Todo.Dtos;
using Tezo_Todo.Models;

namespace Tezo_Todo.Services.Interfaces
{
    public interface IAssignmentRepository
    {
        public Task<List<AssignmentDtos>> GetAllAssignments();
        public Assignment AddAssignment(Guid id, AssignmentDtos task);
        public Task<bool> UpdateAssignment(Guid id, AssignmentDtos assignment);
        public Task<Assignment> DeleteAssignment(Guid id);
        public IEnumerable<Assignment> SearchTask(string searchTerm);
        public IEnumerable<Assignment> SortByDate();
        public IEnumerable<Assignment> FilterByStatus(Status status);
        public IEnumerable<Assignment> FilterByPriority(Priority priority);
        public UserDtos GetUserRespectiveAssignments(Guid id);
        public List<UserDtos> GetAllUserAllAssignments();

    }
}
