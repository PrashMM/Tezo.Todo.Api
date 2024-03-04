
using AutoMapper;
using Tezo_Todo.Data;
using Tezo_Todo.Dtos;
using Tezo_Todo.Models;
using Tezo_Todo.Services.Interfaces;

namespace Tezo_Todo.Services
{
    public class AssignmentServie : IAssignmentService
    {
        private readonly AssignmentRepository assignmentRepository;
        public AssignmentServie(TodoAPIDbContext dbContext, IMapper mapper)
        {
            assignmentRepository = new AssignmentRepository(dbContext, mapper);

        }

        public  Task<List<AssignmentDtos>> GetAllAssignments()
        {
            return  assignmentRepository.GetAllAssignments();
        }

        public Assignment AddAssignment(Guid id, AssignmentDtos task)
        {
            
            return assignmentRepository.AddAssignment(id,task);

        }

        public  Task<bool> UpdateAssignment(Guid id, AssignmentDtos assignment)
        {
            return assignmentRepository.UpdateAssignment(id, assignment);

        }

        public  Task<Assignment> DeleteAssignment(Guid id)
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

        public UserDtos GetUserRespectiveAssignments(Guid id)
        {
            return assignmentRepository.GetUserRespectiveAssignments(id);
        }


        public List<UserDtos> GetAllUserAllAssignments()
        {
            return assignmentRepository.GetAllUserAllAssignments();
        }

        public  Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize)
        {
            return assignmentRepository.GetPaginatedAssignments(pageIndex, pageSize);
        }

    }
}
