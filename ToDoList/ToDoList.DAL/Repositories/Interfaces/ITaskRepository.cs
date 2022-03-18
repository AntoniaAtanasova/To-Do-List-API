using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        public Task<bool> Create(Entities.Task task);

        public Task<bool> Edit(Entities.Task taskToEdit, Entities.Task newTask, string userId);

        public Task<bool> Delete(Entities.Task task);

        public Task<Entities.Task> GetById(int taskId);

        public Task<List<Entities.Task>> GetAllForList(int listId);

        public Task<List<Entities.Task>> GetAllMy(User loggedIn);

        public Task<List<Entities.Task>> GetMyForDate(User loggedIn, DateTime date);

        public Task<bool> Complete(Entities.Task task);

        public Task<bool> Assign(int taskId, string userId);

        public Task<bool> IsAssignedToUser(int taskId, string userId);

        public Task<bool> IsTaskNameTaken(string title, User loggedin);
    }
}
