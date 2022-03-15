using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Repositories.Interfaces;

namespace ToDoList.BLL.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _repo;

        public ToDoListService(IToDoListRepository repo)
        {
            _repo = repo;
        }

        public Task<bool> Create(DAL.Entities.ToDoList toDoList)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Edit(int id, DAL.Entities.ToDoList newList)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<DAL.Entities.ToDoList>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<DAL.Entities.ToDoList> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<DAL.Entities.ToDoList>> GetMy(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Share(int listId, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
