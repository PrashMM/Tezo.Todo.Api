using Tezo.Todo.Dtos;
using Tezo.Todo.Dtos.PaginatedList;
using Tezo.Todo.Models;
using Tezo.Todo.Repository.Interfaces;
using Tezo.Todo.Services.Interfaces;

namespace Tezo.Todo.Services
{
    public class AssignmentService : IAssignmentService
    {
        private IAssignmentRepository assignmentRepository;
        public AssignmentService(IAssignmentRepository assignmentRepo)
        {
            assignmentRepository = assignmentRepo;
        }

        public Task<List<AssignmentDto>> GetAllAssignments()
        {
            return assignmentRepository.GetAllAssignments();
        }

        public Assignment AddAssignment(Guid id, AssignmentDto task)
        {
            return assignmentRepository.AddAssignment(id, task);
        }

        public Task<bool> UpdateAssignment(Guid id, AssignmentDto assignment)
        {
            return assignmentRepository.UpdateAssignment(id, assignment);
        }

        public Task<Assignment> DeleteAssignment(Guid id)
        {
            return assignmentRepository.DeleteAssignment(id);
        }

        public IEnumerable<Assignment> SearchTask(string searchTerm)
        {
            return assignmentRepository.SearchTask(searchTerm);
        }

        public IEnumerable<Assignment> SortByDate()
        {
            return assignmentRepository.SortByDate();
        }

        public IEnumerable<Assignment> FilterByStatus(Status status)
        {
            return assignmentRepository.FilterByStatus(status);
        }

        public IEnumerable<Assignment> FilterByPriority(Priority priority)
        {
            return assignmentRepository.FilterByPriority(priority);
        }

        public UserDto GetUserRespectiveAssignments(Guid id)
        {
            return assignmentRepository.GetUserRespectiveAssignments(id);
        }

        public List<UserDto> GetAllUserAllAssignments()
        {
            return assignmentRepository.GetAllUserAllAssignments();
        }

        public Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize)
        {
            return assignmentRepository.GetPaginatedAssignments(pageIndex, pageSize);
        }

    }
}
