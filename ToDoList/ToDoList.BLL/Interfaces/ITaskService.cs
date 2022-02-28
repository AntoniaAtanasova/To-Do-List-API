using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.BLL.Interfaces
{
    public interface ITaskService
    {
        public Task<List<DAL.Entities.Task>> GetAll();

        public Task<List<DAL.Entities.Task>> GetMy();

        public Task<List<DAL.Entities.Task>> GetMyForDate();

        public Task<DAL.Entities.Task> GetById(int id);

        public Task<bool> Create(DAL.Entities.Task task);

        public Task<bool> Delete(int id);

        public Task<bool> Edit(int id);

        public Task<bool> Assign(int taskId, string userId);
    }
}
