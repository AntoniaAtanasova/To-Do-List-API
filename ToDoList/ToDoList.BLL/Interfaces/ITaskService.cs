using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface ITaskService
    {
        public Task<List<DAL.Entities.Task>> GetAllForList(int listId);

        public Task<List<DAL.Entities.Task>> GetAllMy(User user);

        public Task<List<DAL.Entities.Task>> GetMyForDate(User user, DateTime date);

        public Task<DAL.Entities.Task> GetById(int id);

        public Task<bool> Create(int listId, DAL.Entities.Task task, User user);

        public Task<bool> Delete(int id);

        public Task<bool> Edit(int id, DAL.Entities.Task newTask, string userId);

        public Task<bool> Complete(int id);

        public Task<bool> Assign(int taskId, string userId);
    }
}
