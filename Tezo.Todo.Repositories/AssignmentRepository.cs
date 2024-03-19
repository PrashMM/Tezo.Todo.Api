
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tezo.Todo.Data;
using Tezo.Todo.Dto;
using Tezo.Todo.Dtos;
using Tezo.Todo.Dtos.PaginatedList;
using Tezo.Todo.Models;
using Tezo.Todo.Repositories;
using Tezo.Todo.Repository.Interfaces;

namespace Tezo.Todo.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly TodoAPIDbContext _dbContext;
        private readonly IMapper _mapper;

        public AssignmentRepository(TodoAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<AssignmentDto>> GetAllAssignments()
        {

            if (_dbContext.Assignment.Any())
            {
                var undeletedAssignments = await UndeletedAssignments();
                return _mapper.Map<List<AssignmentDto>>(undeletedAssignments);
            }
            else
            {
                return new List<AssignmentDto>();
            }
        }

        public async Task<Assignment> AddAssignment(Guid id, AssignmentDto task)
        {
            var userDetails = _dbContext.User.FirstOrDefault(u => u.Id == id);
            task.CreatedOn = Extension.GetCurrentDateTime();
            task.UserId = id;  // assigning user Id to assignment userId.
            var assignment = _mapper.Map<Assignment>(task);
            assignment.User = userDetails;

            await _dbContext.Assignment.AddAsync(assignment);
            await _dbContext.SaveChangesAsync();
            return assignment;
        }

        public async Task<bool> UpdateAssignment(Guid id, AssignmentDto assignment)
        {
            // it will automatically map based on primary key i.e assignment ids.
            try
            {
                var task = _mapper.Map<Assignment>(assignment);
                assignment.ModifiedOn = Extension.GetCurrentDateTime();
                task.Id = id;
                var userData = _dbContext.Assignment.Update(task);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Assignment> DeleteAssignment(Guid id)
        {
            var task = await _dbContext.Assignment.FindAsync(id);
            if (task != null && task.IsDeleted == false)
            {
                task.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
            return task;
        }


     
      
        public async Task<List<Assignment>> FilterAssignments(string searchTerm, bool isSort, AssignmentFilter filter)
        {
            var query = _dbContext.Assignment.Where(e => !e.IsDeleted);

            if (!Extension.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(e => EF.Functions.Like(e.Title.ToLower(), $"%{searchTerm.ToLower()}%"));
            }

            // Sorting functionality
            if (isSort)
            {
                query = query.OrderBy(e => e.DueDate);

            }

            // Filtering functionality
            if (filter.status.HasValue)
            {
                query = query.Where(e => e.Status == filter.status.Value);
            }
            if (filter.priority.HasValue)
            {
                query = query.Where(e => e.Priority == filter.priority.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<UserAssignmentsDto> GetUserRespectiveAssignments(Guid id)
        {
            var user = await _dbContext.User.FindAsync(id);
            var tasks = await _dbContext.Assignment.Where(i => i.UserId == id).ToListAsync();
            var users = _mapper.Map<UserAssignmentsDto>(user);
            var assignment = _mapper.Map<List<AssignmentDto>>(tasks);

            users.Assignments = assignment;
            return users;
        }

        public async Task<List<UserAssignmentsDto>> GetUsersAssignments()
        {
            var users = await _dbContext.User.Where(u => !u.IsDeleted).ToListAsync();
            var tasks = await _dbContext.Assignment.Where(e => !e.IsDeleted).ToListAsync();

            var userDtos = _mapper.Map<List<UserDto>>(users);
            var taskDtos = _mapper.Map<List<AssignmentDto>>(tasks);

            var userAssignmentDtos = new List<UserAssignmentsDto>();

            foreach (var userDto in userDtos)
            {
                var user = users.Find(e => e.FirstName == userDto.FirstName && e.LastName == userDto.LastName);
                if (user != null)
                {
                    var userTasks = taskDtos.Where(t => t.UserId == user.Id).ToList();

                    // Filter tasks to include only undeleted assignments
                    userTasks = userTasks.Where(t => !tasks.Any(a => a.Id == t.UserId && a.IsDeleted)).ToList();

                    var userAssignmentsDto = new UserAssignmentsDto
                    {
                        user = userDto,
                        Assignments = userTasks
                    };

                    userAssignmentDtos.Add(userAssignmentsDto);
                }
            }

            return userAssignmentDtos;
        }

        public async Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize)
        {
            // It takes two parameters: pageIndex(the index of the page to retrieve) and pageSize(the number of items per page).
            // It retrieves assignments from the _dbContext.Assignment DbSet.
            // It orders the assignments by their Id.
            // It skips (pageIndex - 1) * pageSize items and takes pageSize items, effectively retrieving the items for the current page.
            // For example, if pageIndex is 1(meaning it's the first page) and pageSize is 10, then no items are skipped (since (1 - 1) * 10 = 0), meaning the first 10 items will be retrieved.
            //If pageIndex is 2, then(2 - 1) * 10 = 10 items are skipped, so it starts retrieving items from the 11th item onward, effectively starting from the second page.

            var assignments = await _dbContext.Assignment.Where(e => !e.IsDeleted)
                .OrderBy(b => b.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _dbContext.Assignment.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            // It counts the total number of assignments in the database using CountAsync().
            // It calculates the total number of pages by dividing the total count by the page size and rounding up using Math.Ceiling.
            // count / (double)pageSize = 35 / 10 = 3.5,&& Math.Ceiling(3.5) = 4

            return new PaginatedList<Assignment>(assignments, pageIndex, totalPages);
        }

        public async Task<List<Assignment>> UndeletedAssignments()
        {
            var undeletedAssignments = await _dbContext.Assignment.Where(a => !a.IsDeleted).ToListAsync();
            return undeletedAssignments;
        }

    }
}
