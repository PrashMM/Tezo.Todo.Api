using Tezo.Todo.Dtos;
using Tezo.Todo.Dtos.PaginatedList;
using Tezo.Todo.Models;

namespace Tezo.Todo.Services.Interfaces
{
    public interface IAssignmentService
    {
        public Task<List<AssignmentDto>> GetAllAssignments();
        public Assignment AddAssignment(Guid id, AssignmentDto task);
        public Task<bool> UpdateAssignment(Guid id, AssignmentDto assignment);
        public Task<Assignment> DeleteAssignment(Guid id);
        public IEnumerable<Assignment> SearchTask(string searchTerm);
        public IEnumerable<Assignment> SortByDate();
        public IEnumerable<Assignment> FilterByStatus(Status status);
        public IEnumerable<Assignment> FilterByPriority(Priority priority);
        public UserDto GetUserRespectiveAssignments(Guid id);
        public List<UserDto> GetAllUserAllAssignments();
        public Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize);

    }
}
