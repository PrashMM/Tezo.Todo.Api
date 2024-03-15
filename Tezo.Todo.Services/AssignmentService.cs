using System.Text;
using Tezo.Todo.Dto;
using Tezo.Todo.Dtos;
using Tezo.Todo.Dtos.PaginatedList;
using Tezo.Todo.Models;
using Tezo.Todo.Repository.Interfaces;
using Tezo.Todo.Services.Interfaces;

namespace Tezo.Todo.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository assignmentRepository;
        public AssignmentService(IAssignmentRepository assignmentRepo)
        {
            assignmentRepository = assignmentRepo;
        }

        public async Task<List<AssignmentDto>> GetAllAssignments()
        {
            return await assignmentRepository.GetAllAssignments();
        }

        public async Task<Assignment> AddAssignment(Guid id, AssignmentDto task)
        {
            return await assignmentRepository.AddAssignment(id, task);
        }

        public async Task<bool> UpdateAssignment(Guid id, AssignmentDto assignment)
        {
            return await assignmentRepository.UpdateAssignment(id, assignment);
        }

        public async Task<Assignment> DeleteAssignment(Guid id)
        {
            return await assignmentRepository.DeleteAssignment(id);
        }

        public async Task<List<Assignment>> SearchTask(string searchTerm)
        {
            return await assignmentRepository.SearchTask(searchTerm);
        }

        public async Task<List<Assignment>> SortByDate()
        {
            return await assignmentRepository.SortByDate();
        }

        public async Task<List<Assignment>> FilterAssignments(AssignmentFilterModel filter)
        {
            return await assignmentRepository.FilterAssignments(filter);
        }

        public async Task<UserAssignmentsDto> GetUserRespectiveAssignments(Guid id)
        {
            return await assignmentRepository.GetUserRespectiveAssignments(id);
        }

        public async Task<List<UserAssignmentsDto>> GetUsersAssignments()
        {
            return await assignmentRepository.GetUsersAssignments();
        }

        public async Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize)
        {
            return await assignmentRepository.GetPaginatedAssignments(pageIndex, pageSize);
        }
    }
}
