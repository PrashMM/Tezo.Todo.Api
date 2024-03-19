﻿
using Tezo.Todo.Dto;
using Tezo.Todo.Dtos;
using Tezo.Todo.Dtos.PaginatedList;
using Tezo.Todo.Models;

namespace Tezo.Todo.Repository.Interfaces
{
    public interface IAssignmentRepository
    {
        public Task<List<AssignmentDto>> GetAllAssignments();
        public Task<Assignment> AddAssignment(Guid id, AssignmentDto task);
        public Task<bool> UpdateAssignment(Guid id, AssignmentDto assignment);
        public Task<Assignment> DeleteAssignment(Guid id);
        public Task<List<Assignment>> FilterAssignments(string searchTerm, bool isSort, AssignmentFilter filter);
        public Task<UserAssignmentsDto> GetUserRespectiveAssignments(Guid id);
        public Task<List<UserAssignmentsDto>> GetUsersAssignments();
        public Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize);

    }
}
