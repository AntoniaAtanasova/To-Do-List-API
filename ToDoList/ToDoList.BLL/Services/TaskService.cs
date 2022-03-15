using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.Interfaces;

namespace ToDoList.BLL.Services
{
    public class TaskService : ITaskService
    {
        public Task<bool> Assign(int taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(DAL.Entities.Task task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(int id, DAL.Entities.Task newTask)
        {
            throw new NotImplementedException();
        }

        public Task<List<DAL.Entities.Task>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DAL.Entities.Task> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DAL.Entities.Task>> GetMy()
        {
            throw new NotImplementedException();
        }

        public Task<List<DAL.Entities.Task>> GetMyForDate()
        {
            throw new NotImplementedException();
        }
    }
}
