using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        public Task<bool> Create(Entities.Task task);

        public Task<bool> Edit(int taskId, Entities.Task newTask, string userId);

        public Task<bool> Delete(int taskId);

        public Task<Entities.Task> GetById(int taskId);

        public Task<List<Entities.Task>> GetAll();

        public Task<List<Entities.Task>> GetMy(User loggedIn);

        public Task<List<Entities.Task>> GetMyForDate(DateTime date, User loggedIn);

        public Task<bool> Complete(int taskId);

        public Task<bool> Assign(int taskId, string userId);

        public Task<bool> IsTaskNameTaken(string title, User loggedin);
    }
}
