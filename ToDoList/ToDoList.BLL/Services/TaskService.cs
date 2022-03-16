using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Repositories.Interfaces;

namespace ToDoList.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IHolidayRepository _holidayRepo;

        public TaskService(ITaskRepository taskRepo, IHolidayRepository holidayRepo)
        {
            _taskRepo = taskRepo;
            _holidayRepo = holidayRepo;
        }

        public async Task<bool> Assign(int taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Complete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(DAL.Entities.Task task, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(int id, DAL.Entities.Task newTask, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<DAL.Entities.Task>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<DAL.Entities.Task> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DAL.Entities.Task>> GetMy(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<DAL.Entities.Task>> GetMyForDate(User user, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
