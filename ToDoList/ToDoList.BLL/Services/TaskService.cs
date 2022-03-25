using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.BLL.Exceptions;
using ToDoList.BLL.Interfaces;
using ToDoList.Common;
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
            if (await _taskRepo.IsAssignedToUser(taskId, userId))
            {
                throw new ToDoListException(taskId, Constants.TaskAlreadyAssigned, Constants.BadRequest);
            }

            return await _taskRepo.Assign(taskId, userId);
        }

        public async Task<bool> Complete(int id)
        {
            var task = await _taskRepo.GetById(id);

            if (task == null)
            {
                throw new ToDoListException(Constants.TaskNotFound, Constants.NotFound);
            }

            return await _taskRepo.Complete(task);
        }

        public async Task<bool> Create(int listId, DAL.Entities.Task task, User user)
        {
            if (!await _taskRepo.IsTaskNameTaken(task.Title, user))
            {
                List<Holiday> holidays = await _holidayRepo.GetAll();

                if (holidays.Any(h => h.Date == task.Date))
                {
                    task.Day = DayType.Holiday;
                }
                else if (task.Date.DayOfWeek == DayOfWeek.Saturday
                    || task.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    task.Day = DayType.Weekend;
                }
                else
                {
                    task.Day = DayType.WorkDay;
                }

                task.ToDoListId = listId;
                task.CreatedBy = user.Id;
                task.LastModifiedBy = user.Id;

                return await _taskRepo.Create(task);
            }

            throw new ToDoListException(task.Title, Constants.TaskTitleTaken, Constants.BadRequest);
        }

        public async Task<bool> Delete(int id)
        {
            var task = await _taskRepo.GetById(id);

            if (task == null)
            {
                throw new ToDoListException(Constants.TaskNotFound, Constants.NotFound);
            }

            return await _taskRepo.Delete(task);
        }

        public async Task<bool> Edit(int id, DAL.Entities.Task newTask, string userId)
        {
            var task = await _taskRepo.GetById(id);

            if (task == null)
            {
                throw new ToDoListException(Constants.TaskNotFound, Constants.NotFound);
            }

            return await _taskRepo.Edit(task, newTask, userId);
        }

        public Task<List<DAL.Entities.Task>> GetAllForList(int listId)
        {
            return _taskRepo.GetAllForList(listId);
        }

        public async Task<DAL.Entities.Task> GetById(int id)
        {
            var task = await _taskRepo.GetById(id);

            if (task == null)
            {
                throw new ToDoListException(Constants.TaskNotFound, Constants.NotFound);
            }

            return task;
        }

        public Task<List<DAL.Entities.Task>> GetAllMy(User user)
        {
            return _taskRepo.GetAllMy(user);
        }

        public Task<List<DAL.Entities.Task>> GetMyForDate(User user, DateTime date)
        {
            return _taskRepo.GetMyForDate(user, date);
        }
    }
}