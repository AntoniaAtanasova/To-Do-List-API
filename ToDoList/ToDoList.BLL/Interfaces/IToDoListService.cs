using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface IToDoListService
    {
        public Task<List<DAL.Entities.ToDoList>> GetAll();

        public Task<List<DAL.Entities.ToDoList>> GetMy(User user);

        public Task<DAL.Entities.ToDoList> GetById(int id);

        public Task<bool> Create(DAL.Entities.ToDoList toDoList, User user);

        public Task<bool> Delete(int id);

        public Task<bool> Edit(int id, DAL.Entities.ToDoList newList, string userId);

        public Task<bool> Share(int listId, string userId);
    }
}
