using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DAL.Repositories.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly DatabaseContext _databaseContext;

        public TaskRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> Complete(Entities.Task task)
        {
            task.IsComplete = true;

            _databaseContext.Update(task);
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Create(Entities.Task task)
        {
            await _databaseContext.Tasks.AddAsync(task);
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Entities.Task task)
        {
            _databaseContext.Tasks.Remove(task);
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(Entities.Task taskToEdit, Entities.Task newTask, string userId)
        {
            taskToEdit.Title = newTask.Title;
            taskToEdit.Description = newTask.Description;
            taskToEdit.LastModifiedBy = userId;
            taskToEdit.LastModifiedAt = DateTime.Now;
            taskToEdit.Date = newTask.Date;

            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Entities.Task>> GetAllMy(Entities.User loggedIn)
        {
            var myTasks = await _databaseContext.Tasks.Where(t => t.CreatedBy == loggedIn.Id).ToListAsync();
            var assignedTasks = await _databaseContext.Tasks.Where(t => t.Users.Contains(loggedIn)).ToListAsync();

            var allMy = myTasks.Concat(assignedTasks).ToList();
            return allMy;
        }

        public async Task<Entities.Task> GetById(int taskId)
        {
            return await _databaseContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<List<Entities.Task>> GetAllForList(int listId)
        {
            return await _databaseContext.Tasks.Where(t => t.ToDoListId == listId).ToListAsync();
        }

        public async Task<List<Entities.Task>> GetMyForDate(Entities.User loggedIn, DateTime date)
        {
            var tasks = await GetAllMy(loggedIn);
            return tasks.Where(t => t.Date == date).ToList();
        }

        public async Task<bool> IsTaskNameTaken(string title, Entities.User loggedIn)
        {
            var tasks = await GetAllMy(loggedIn);

            return tasks.Any(t => t.Title == title);
        }

        public async Task<bool> Assign(int taskId, string userId)
        {
            await _databaseContext.AssignedTasks.AddAsync(new Entities.AssignedTask()
            {
                UserId = userId,
                TaskId = taskId
            });
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsAssignedToUser(int taskId, string userId)
        {
            return await _databaseContext.AssignedTasks.AnyAsync(at => at.UserId == userId && at.TaskId == taskId);
        }
    }
}
