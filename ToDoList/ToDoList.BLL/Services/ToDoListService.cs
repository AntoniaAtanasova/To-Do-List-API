using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Services
{
    public class ToDoListService : IToDoListService
    {
        public Task<bool> Create(DAL.Entities.ToDoList toDoList)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Edit(int id)
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

        public Task<List<DAL.Entities.ToDoList>> GetMy()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<DAL.Entities.ToDoList>> GetMy(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<DAL.Entities.ToDoList>> GetShared()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Share(int listId, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
