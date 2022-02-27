using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoList.BLL.Interfaces
{
    public interface IToDoListService
    {
        public Task<List<DAL.Entities.ToDoList>> GetMy();

        public Task<List<DAL.Entities.ToDoList>> GetShared();

        public Task<DAL.Entities.ToDoList> GetById(int id);

        public Task<bool> Create(DAL.Entities.ToDoList toDoList);

        public Task<bool> Delete(int id);

        public Task<bool> Edit(int id);

        public Task<bool> Share(int listId, string userId);
    }
}
