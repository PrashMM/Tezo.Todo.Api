
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tezo_Todo.Data;
using Tezo_Todo.Dtos;
using Tezo_Todo.Models;
using Tezo_Todo.Services.Interfaces;

namespace Tezo_Todo.Services
{
    public class AssignmentServie : IAssignmentService
    {
        private TodoAPIDbContext _dbContext;
        private IMapper _mapper;
        public AssignmentServie(TodoAPIDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<AssignmentDtos>> GetAllAssignments()
        {
            return _mapper.Map<List<AssignmentDtos>>(_dbContext.Assignment.ToList());
        }

        public Assignment AddAssignment(Guid id, AssignmentDtos task)
        {
            var userData = _dbContext.User.FirstOrDefault(u => u.Id == id);
            task.UserId = id;
            var assignment = _mapper.Map<Assignment>(task);
            assignment.User = userData;

            _dbContext.Assignment.AddAsync(assignment);
            _dbContext.SaveChangesAsync();
            return assignment;

        }

        public async Task<bool> UpdateAssignment(Guid id, AssignmentDtos assignment)
        {
            //var task11 = await _dbContext.Assignment.FindAsync(id);
            try
            {
                var task = _mapper.Map<Assignment>(assignment);
                task.Id = id;
                var userData = _dbContext.Assignment.Update(task);
                _dbContext.SaveChangesAsync();
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
            if (task != null)
            {
                _dbContext.Assignment.Remove(task);
                await _dbContext.SaveChangesAsync();

            }
            return task;

        }


        public IEnumerable<Assignment> SearchTask(string searchTerm)
        {
            var tasks = _dbContext.Assignment.Where(e => EF.Functions.Like(e.Title.ToLower(), $"%{searchTerm.ToLower()}%")).ToList();

            foreach (var task in tasks)
            {
                _dbContext.Assignment.AddAsync(task);
            }
            return tasks;
        }

        public IEnumerable<Assignment> SortByDate()
        {
            var tasks = _dbContext.Assignment.OrderBy(e => e.DueDate);
            foreach (var task in tasks)
            {
                _dbContext.Assignment.AddAsync(task);
                _dbContext.SaveChangesAsync();
            }
            return tasks;
        }

        public IEnumerable<Assignment> FilterByStatus(Status status)
        {
            var tasks = _dbContext.Assignment.Where(e => e.Status == status);

            foreach (var task in tasks)
            {
                _dbContext.Assignment.AddAsync(task);
            }
            return tasks;
        }

        public IEnumerable<Assignment> FilterByPriority(Priority priority)
        {
            var tasks = _dbContext.Assignment.Where(e => e.Priority == priority);

            foreach (var task in tasks)
            {
                _dbContext.Assignment.AddAsync(task);
            }
            return tasks;
        }

        public UserDtos GetUserRespectiveAssignments(Guid id)
        {
            var user = _dbContext.User.Find(id);
            var tasks = _dbContext.Assignment.Where(i => i.UserId == id);
            var users = _mapper.Map<UserDtos>(user);
            var assignment = _mapper.Map<List<AssignmentDtos>>(tasks);

            users.Assignments = assignment;
            return users;
        }


        public List<UserDtos> GetAllUserAllAssignments()
        {
            var users = _dbContext.User.ToList();
            var tasks = _dbContext.Assignment.ToList();
            var userDtos = _mapper.Map<List<UserDtos>>(users);
            var taskDtos = _mapper.Map<List<AssignmentDtos>>(tasks);

            foreach (var userDto in userDtos)
            {
                var user = users.Find(e => e.UniqueName == userDto.UniqueName);
                if (user != null)
                {
                    var userTasks = taskDtos.Where(t => t.UserId == user.Id).ToList();

                    userDto.Assignments = [];
                    userDto.Assignments.AddRange(userTasks);

                }
            }

            return userDtos;
        }

    }
}
