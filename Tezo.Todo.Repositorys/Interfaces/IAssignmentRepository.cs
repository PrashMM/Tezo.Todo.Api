using Tezo.Todo.Dtos;
using Tezo.Todo.Dtos.PaginatedList;
using Tezo.Todo.Models;

namespace Tezo.Todo.Repository.Interfaces
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
        public Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize);

    }
}
