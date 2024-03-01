
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using System;
//using Tezo_Todo.Data;
//using Tezo_Todo.Dtos;
//using Tezo_Todo.Models;
//using Tezo_Todo.Services.Interfaces;

//namespace Tezo_Todo.Services
//{
//    public class AssignmentServie : IAssignmentService
//    {
//        private TodoAPIDbContext _dbContext;
//        private IMapper _mapper;
//        public AssignmentServie(TodoAPIDbContext dbContext, IMapper mapper)
//        {
//            _dbContext = dbContext;
//            this._mapper = mapper;
//        }

//        public async Task<List<AssignmentDtos>> GetAllAssignments()
//        {
//            // Here Data will be returned in Destinationa format. -> _mapper.Map<Destination>(Source);  
//            return _mapper.Map<List<AssignmentDtos>>(_dbContext.Assignment.ToList());
//        }

//        public Assignment AddAssignment(Guid id, AssignmentDtos task)
//        {
//            var userDetails = _dbContext.User.FirstOrDefault(u => u.Id == id);
//            task.UserId = id;  // assigning user Id to assignment userId.
//            var assignment = _mapper.Map<Assignment>(task);
//            assignment.User = userDetails;

//            _dbContext.Assignment.AddAsync(assignment);
//            _dbContext.SaveChangesAsync();
//            return assignment;

//        }

//        public async Task<bool> UpdateAssignment(Guid id, AssignmentDtos assignment)
//        {
//            // it will automatically map based on primary key i.e assignment ids.
//            try
//            {
//                var task = _mapper.Map<Assignment>(assignment);
//                task.Id = id;
//                var userData = _dbContext.Assignment.Update(task);
//                _dbContext.SaveChangesAsync();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }

//        }

//        public async Task<Assignment> DeleteAssignment(Guid id)
//        {
//            var task = await _dbContext.Assignment.FindAsync(id);
//            if (task != null)
//            {
//                _dbContext.Assignment.Remove(task);
//                await _dbContext.SaveChangesAsync();
//            }
//            return task;

//        }


//        public IEnumerable<Assignment> SearchTask(string searchTerm)
//        {
//            // EF.Functions.Like(...): an Entity Framework function used to perform a SQL LIKE comparison.It's being used to check if the Title of each assignment contains the searchTerm provided by the user.
//            var tasks = _dbContext.Assignment.Where(e => EF.Functions.Like(e.Title.ToLower(), $"%{searchTerm.ToLower()}%")).ToList();

//            foreach (var task in tasks)
//            {
//                _dbContext.Assignment.AddAsync(task);
//            }
//            return tasks;
//        }

//        public IEnumerable<Assignment> SortByDate()
//        {
//            var tasks = _dbContext.Assignment.OrderBy(e => e.DueDate);
//            foreach (var task in tasks)
//            {
//                _dbContext.Assignment.AddAsync(task);
//                _dbContext.SaveChangesAsync();
//            }
//            return tasks;
//        }

//        public IEnumerable<Assignment> FilterByStatus(Status status)
//        {
//            var tasks = _dbContext.Assignment.Where(e => e.Status == status);

//            foreach (var task in tasks)
//            {
//                _dbContext.Assignment.AddAsync(task);
//            }
//            return tasks;
//        }

//        public IEnumerable<Assignment> FilterByPriority(Priority priority)
//        {
//            var tasks = _dbContext.Assignment.Where(e => e.Priority == priority);

//            foreach (var task in tasks)
//            {
//                _dbContext.Assignment.AddAsync(task);
//            }
//            return tasks;
//        }

//        public UserDtos GetUserRespectiveAssignments(Guid id)
//        {
//            var user = _dbContext.User.Find(id);
//            var tasks = _dbContext.Assignment.Where(i => i.UserId == id);
//            var users = _mapper.Map<UserDtos>(user);
//            var assignment = _mapper.Map<List<AssignmentDtos>>(tasks);

//            users.Assignments = assignment;
//            return users;
//        }


//        public List<UserDtos> GetAllUserAllAssignments()
//        {
//            var users = _dbContext.User.ToList();
//            var tasks = _dbContext.Assignment.ToList();
//            var userDtos = _mapper.Map<List<UserDtos>>(users);
//            var taskDtos = _mapper.Map<List<AssignmentDtos>>(tasks);

//            foreach (var userDto in userDtos)
//            {
//                var user = users.Find(e => e.UniqueName == userDto.UniqueName);
//                if (user != null)
//                {
//                    var userTasks = taskDtos.Where(t => t.UserId == user.Id).ToList();

//                    userDto.Assignments = [];
//                    userDto.Assignments.AddRange(userTasks);

//                }
//            }

//            return userDtos;
//        }

//        public async Task<PaginatedList<Assignment>> GetPaginatedAssignments(int pageIndex, int pageSize)
//        {
//            // It takes two parameters: pageIndex(the index of the page to retrieve) and pageSize(the number of items per page).
//            // It retrieves assignments from the _dbContext.Assignment DbSet.
//            // It orders the assignments by their Id.
//            // It skips (pageIndex - 1) * pageSize items and takes pageSize items, effectively retrieving the items for the current page.
//            // For example, if pageIndex is 1(meaning it's the first page) and pageSize is 10, then no items are skipped (since (1 - 1) * 10 = 0), meaning the first 10 items will be retrieved.
//            //If pageIndex is 2, then(2 - 1) * 10 = 10 items are skipped, so it starts retrieving items from the 11th item onward, effectively starting from the second page.

//            var assignments = await _dbContext.Assignment
//                .OrderBy(b => b.Id)
//                .Skip((pageIndex - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();

//            var count = await _dbContext.Assignment.CountAsync();
//            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
//            // It counts the total number of assignments in the database using CountAsync().
//            // It calculates the total number of pages by dividing the total count by the page size and rounding up using Math.Ceiling.
//            // count / (double)pageSize = 35 / 10 = 3.5,&& Math.Ceiling(3.5) = 4

//            return new PaginatedList<Assignment>(assignments, pageIndex, totalPages);
//        }

//    }
//}
